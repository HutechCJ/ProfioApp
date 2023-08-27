using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Profio.Infrastructure.Cache;
using Profio.Infrastructure.Filters;
using Profio.Infrastructure.HealthCheck;
using Profio.Infrastructure.Identity;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.OpenTelemetry;
using Profio.Infrastructure.Persistence.Graph;
using Profio.Infrastructure.Persistence.Relational;
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
        .AllowAnyMethod()
        .AllowAnyHeader()));

    services.AddOpenApi();

    services.AddMediatR(config =>
      config.RegisterServicesFromAssembly(AssemblyReference.ExecuteAssembly)
    );

    services.AddNeo4J(builder.Configuration);

    services
      .AddProblemDetails()
      .AddEndpointsApiExplorer();

    services.AddRedisCache(builder, builder.Configuration);
    builder.AddSerilog();
    builder.AddOpenTelemetry();
    builder.AddHealthCheck();

    services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();

    services.AddDbContext<DbContext, ApplicationDbContext>(options =>
    {
      options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    });

    services.AddIdentityCore<ApplicationUser>(options =>
    {
      options.Password.RequireDigit = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireNonAlphanumeric = true;
      options.Password.RequireUppercase = true;
      options.Password.RequiredLength = 6;
      options.Password.RequiredUniqueChars = 1;

      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
      options.Lockout.MaxFailedAccessAttempts = 5;
      options.Lockout.AllowedForNewUsers = true;

      options.User.RequireUniqueEmail = true;
    })
      .AddEntityFrameworkStores<ApplicationDbContext>();

    services.AddScoped<ApplicationDbContextInitializer>();
  }

  public static async Task UseWebInfrastructureAsync(this WebApplication app)
  {

    if (app.Environment.IsDevelopment())
    {

      using var scope = app.Services.CreateScope();
      var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
      await initialiser.InitialiseAsync();
      await initialiser.SeedAsync();
    }

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
