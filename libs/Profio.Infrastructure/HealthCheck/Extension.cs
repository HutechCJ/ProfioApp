using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Profio.Infrastructure.Persistence;

namespace Profio.Infrastructure.HealthCheck;

public static class Extension
{
  public static WebApplicationBuilder AddHealthCheck(this WebApplicationBuilder builder)
  {
    builder.Services.AddSingleton<HealthService>();
    builder.Services.AddHealthChecks()
      .AddCheck<HealthCheck>("Health Check", tags: new[] { "health check" })
      .AddDbContextCheck<ApplicationDbContext>(tags: new[] { "db context" })
      .AddRedis(builder.Configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException(), tags: new[] { "redis" })
      .AddNpgSql(builder.Configuration.GetConnectionString("Postgres")
                 ?? throw new InvalidOperationException(), tags: new[] { "database" })
      .AddRabbitMQ(builder.Configuration.GetSection("MessageBroker")["FullUrl"]
                   ?? throw new InvalidOperationException(),
        name: "RabbitMq",
        tags: new[] { "message broker" })
      .AddSignalRHub(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development
          ? "https://localhost:9023/current-location"
          : $"https://{Environment.ExpandEnvironmentVariables("%WEBSITE_SITE_NAME%")}.azurewebsites.net/current-location",
        "SignalR",
        tags: new[] { "hub" });

    builder.Services
      .AddHealthChecksUI(options =>
      {
        options.AddHealthCheckEndpoint("Health Check API", "/hc");
        options.SetEvaluationTimeInSeconds(60);
        options.SetApiMaxActiveRequests(1);
        options.DisableDatabaseMigrations();
        options.MaximumHistoryEntriesPerEndpoint(50);
        options.SetNotifyUnHealthyOneTimeUntilChange();
        options.UseApiEndpointHttpMessageHandler(_ =>
        {
          return new()
          {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
          };
        });
      })
      .AddInMemoryStorage();

    return builder;
  }

  public static void MapHealthCheck(this WebApplication app)
  {
    app.MapHealthChecks("/hc", new()
    {
      Predicate = _ => true,
      ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
      AllowCachingResponses = false,
      ResultStatusCodes =
      {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
      }
    });
    app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
  }
}
