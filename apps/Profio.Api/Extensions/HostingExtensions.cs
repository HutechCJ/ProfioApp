using System.IO.Compression;
using System.Net.Mime;
using System.Text.Json.Serialization;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Serialization;
using Profio.Application;
using Profio.Infrastructure;
using Profio.Infrastructure.Filters;
using Profio.Infrastructure.Persistence;
using Profio.Infrastructure.Swagger;

namespace Profio.Api.Extensions;

public static class HostingExtensions
{
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {
    FirebaseApp.Create(new AppOptions
    {
      Credential = GoogleCredential.FromFile("firebase.json")
    });

    builder.Services.AddControllers(options =>
      {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
        options.Filters.Add<LoggingFilter>();
        options.Filters.Add<ExceptionFilter>();
      })
      .AddNewtonsoftJson(options =>
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new CamelCaseNamingStrategy
          {
            ProcessDictionaryKeys = true
          }
        })
      .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
      )
      .AddApplicationPart(AssemblyReference.Assembly);

    builder.Services.AddResponseCompression(options =>
      {
        options.EnableForHttps = true;
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
        {
          MediaTypeNames.Application.Json,
          MediaTypeNames.Text.Plain,
          MediaTypeNames.Image.Jpeg
        });
      })
      .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
      .AddResponseCaching(options => options.MaximumBodySize = 1024)
      .AddRouting(options => options.LowercaseUrls = true)
      .Configure<FormOptions>(options =>
        options.MultipartBodyLengthLimit = 50000000
      );

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder);
    builder.Services.AddRateLimiting();

    builder.Services.AddCors(options => options
      .AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

    builder.WebHost.ConfigureKestrel(options =>
    {
      options.AddServerHeader = false;
      options.AllowResponseHeaderCompression = true;
      options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
    });

    return builder.Build();
  }

  public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
  {
    var migration = new MigrationBuilder("Profio.Infrastructure.Persistence.Migrations");
    migration.MigrateDataFromScript();

    app.UseOpenApi()
      .UseDeveloperExceptionPage()
      .UseRedocly()
      .UseHsts();

    if (app.Environment.IsProduction())
      app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.MapControllers()
      .RequirePerUserRateLimit();
    app.UseRateLimiter();

    await app.UseWebInfrastructureAsync();
    await app.DoDbMigrationAsync(app.Logger);

    return app;
  }
}
