using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Profio.Infrastructure.Jobs;

public static class Extension
{
  public static void AddHangFire(this WebApplicationBuilder builder)
  {
    builder.Services.AddHangfire(cfg =>
      cfg
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseDefaultTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseRedisStorage(builder.Configuration.GetConnectionString("Redis"))
    );

    builder.Services.AddHangfireServer();
  }

  public static void UseHangFire(this WebApplication app)
  {
    app.UseHangfireDashboard();
    app.UseHangfireDashboard("/jobs", new DashboardOptions
    {
      Authorization = new[] { new HangFireAuthorizationFilter() },
      IgnoreAntiforgeryToken = true
    });
  }
}
