using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using Profio.Infrastructure.Searching;
using Profio.Infrastructure.Swagger;
using Profio.Infrastructure.Versioning;
using System.IO.Compression;
using System.Net.Mime;
using Profio.Infrastructure.Key;

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
        options.Filters.Add<ExceptionMiddleware>();
      })
      .AddNewtonsoftJson()
      .AddApplicationPart(AssemblyReference.Assembly);

    services.AddResponseCompression(options =>
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
    builder.AddLuceneSearch();

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer()
      .AddOpenApi();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
    services.AddScoped<ITokenService, TokenService>();

    services.AddPostgres(builder.Configuration)
      .AddRedisCache(builder.Configuration)
      .AddEventBus(builder.Configuration);

    services.AddMqttBus(builder.Configuration);

    services.AddApplicationIdentity(builder);

    services.AddApiKey();
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
    app.Map("/", () => Results.Redirect("/api-docs"));
    app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: 500))
      .ExcludeFromDescription();
    app.MapWhen(context => context.Response.StatusCode == StatusCodes.Status401Unauthorized, appBuilder =>
    {
      appBuilder.Run(async context =>
      {
        var problemDetails = new ProblemDetails
        {
          Title = "Unauthorized",
          Detail = "You are not authorized to access this resource.",
          Status = StatusCodes.Status401Unauthorized,
          Type = "https://httpstatuses.com/401"
        };
        await context.Response.WriteAsJsonAsync(problemDetails);
      });
    });
  }
}
