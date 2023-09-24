using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore.Migrations;
using Profio.Application;
using Profio.Infrastructure;
using Profio.Infrastructure.Persistence;
using Profio.Infrastructure.Swagger;

namespace Profio.Api.Extensions;

public static class HostingExtensions
{
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {
    FirebaseApp.Create(new AppOptions()
    {
      Credential = GoogleCredential.FromFile("firebase.json")
    });

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder);
    builder.Services.AddRateLimiting();


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
