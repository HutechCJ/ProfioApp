using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Cache;
using Profio.Infrastructure.CQRS;
using Profio.Infrastructure.Filters;
using Profio.Infrastructure.HealthCheck;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Infrastructure.Persistence.Graph;
using Profio.Infrastructure.Swagger;

namespace Profio.Infrastructure;

public static class ConfigureServices
{
  public static void AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services.AddControllers(options =>
      {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
        options.Filters.Add<LoggingFilter>();
      })
      .AddNewtonsoftJson()
      .AddApplicationPart(AssemblyReference.Assembly);

    services.AddResponseCompression(options =>
      {
        options.EnableForHttps = true;
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
        {
          "application/json",
          "application/xml",
          "text/plain",
          "image/png",
          "image/jpeg"
        });
      })
      .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
      .AddResponseCaching(options => options.MaximumBodySize = 1024)
      .AddRouting(options => options.LowercaseUrls = true);

    services.AddCors(options => options
      .AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

    services.AddNeo4J(builder.Configuration);

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer()
      .AddOpenApi()
      .AddMediator();

    services.AddAutoMapper(AssemblyReference.AppDomainAssembly);
    services.AddRedisCache(builder, builder.Configuration);
    builder.AddSerilog();
    builder.AddOpenTelemetry();
    builder.AddHealthCheck();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
  }

  public static void UseWebInfrastructure(this WebApplication app)
  {

    app.UseCors()
      .UseExceptionHandler()
      .UseHttpsRedirection()
      .UseRateLimiter()
      .UseResponseCaching()
      .UseResponseCompression()
      .UseStatusCodePages()
      .UseStaticFiles();
    app.MapHealthCheck();
    app.Map("/", () => Results.Redirect("/swagger"));
    app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: 500))
      .ExcludeFromDescription();
  }
}
