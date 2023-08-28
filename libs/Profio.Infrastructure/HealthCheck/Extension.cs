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
                    ?? throw new InvalidOperationException(), tags: new[] { "database" });

    builder.Services
      .AddHealthChecksUI(options =>
      {
        options.AddHealthCheckEndpoint("Health Check API", "/hc");
        options.SetEvaluationTimeInSeconds(60);
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
    app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
  }
}
