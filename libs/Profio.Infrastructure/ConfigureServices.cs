using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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
using System.IO.Compression;
using System.Text;

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
    builder.AddHangFire();
    builder.AddSocketHub();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
    services.AddScoped<ITokenService, TokenService>();

    services.AddPostgres(builder.Configuration);
    services.AddEventBus(builder.Configuration);
    services.AddIdentity();

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:TokenKey"] ?? string.Empty));

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(5)
          };
        });
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

    app
      .UseHangFire();

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
