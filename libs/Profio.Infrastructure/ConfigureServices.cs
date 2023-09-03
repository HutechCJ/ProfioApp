using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Profio.Infrastructure.Bus;
using Profio.Infrastructure.Cache;
using Profio.Infrastructure.Filters;
using Profio.Infrastructure.HealthCheck;
using Profio.Infrastructure.Hub;
using Profio.Infrastructure.Identity;
using Profio.Infrastructure.Jobs;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.Middleware;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Infrastructure.Persistence;
using Profio.Infrastructure.Swagger;
using Profio.Infrastructure.Versioning;
using System.IO.Compression;

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
        .WithMethods(
          HttpMethods.Get,
          HttpMethods.Post,
          HttpMethods.Put,
          HttpMethods.Patch,
          HttpMethods.Delete,
          HttpMethods.Options
        )
        .AllowAnyHeader()));

    builder.AddApiVersioning();
    builder.AddSerilog();
    builder.AddOpenTelemetry();
    builder.AddHealthCheck();
    builder.AddHangFire();
    builder.AddSocketHub();

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer()
      .AddOpenApi();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
    services.AddScoped<ITokenService, TokenService>();

    services.AddPostgres(builder.Configuration)
      .AddRedisCache(builder.Configuration)
      .AddEventBus(builder.Configuration);

    services.AddApplicationIdentity(builder);
  }

  public static async Task UseWebInfrastructureAsync(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      using var scope = app.Services.CreateScope();
      var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
      await initializer.InitialiseAsync();
      await initializer.SeedAsync();
    }

    app
      .UseAuthentication()
      .UseAuthorization();

    app
      .UseMiddleware<ExceptionMiddleware>()
      .UseMiddleware<TimeOutMiddleware>()
      .UseMiddleware<XssProtectionMiddleware>();

    app.UseHangFire();
    app.MapLocationHub();

    app
      .UseCors()
      .UseExceptionHandler()
      .UseHttpsRedirection()
      .UseRateLimiter()
      .UseResponseCaching()
      .UseResponseCompression()
      .UseStatusCodePages()
      .UseStaticFiles();

    app.MapHealthCheck();
    app.Map("/", () => Results.Redirect("/swagger"));
    app.Map("/redoc", () => Results.Redirect("/api-docs"));
    app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: 500))
      .ExcludeFromDescription();
  }
}
