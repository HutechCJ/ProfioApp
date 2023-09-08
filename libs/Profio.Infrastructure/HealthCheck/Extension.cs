using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
      .AddRabbitMQ("amqps://hfzbnoni:03X2irdDUlSBV7D4SoQ4NFNZZ2YglnEh@octopus.rmq3.cloudamqp.com/hfzbnoni",
        name: "RabbitMq",
        tags: new[] { "message broker" })
      .AddHangfire(_ => { }, name: "HangFire", tags: new[] { "jobs" })
      .AddSignalRHub(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
        ? "https://localhost:9023/current-location"
        : $"https://{Environment.ExpandEnvironmentVariables("%WEBSITE_SITE_NAME%")}.azurewebsites.net/current-location",
        name: "SignalR",
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
        [HealthStatus.Healthy] = 200,
        [HealthStatus.Degraded] = 200,
        [HealthStatus.Unhealthy] = 503
      }
    });
    app.MapHealthChecksUI(options => options.UIPath = "/hc-ui").RequireAuthorization();
  }
}
