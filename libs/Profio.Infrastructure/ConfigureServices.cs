using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Profio.Infrastructure.Cache;
using Profio.Infrastructure.Filters;
using Profio.Infrastructure.HealthCheck;
using Profio.Infrastructure.Identity;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.Middleware;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Infrastructure.Persistence;
using Profio.Infrastructure.Swagger;
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
        ).AllowAnyHeader()));

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer()
      .AddOpenApi();

    services.AddRedisCache(builder, builder.Configuration);

    builder.AddSerilog();
    builder.AddOpenTelemetry();
    builder.AddHealthCheck();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();

    services.AddPostgres(builder.Configuration);
    services.AddIdentity();
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

    app.UseMiddleware<ExceptionMiddleware>()
      .UseMiddleware<TimeOutMiddleware>()
      .UseMiddleware<XssProtectionMiddleware>();

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
    app.Map("/redoc", () => Results.Redirect("/api-docs"));
    app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: 500))
      .ExcludeFromDescription();
  }
}
