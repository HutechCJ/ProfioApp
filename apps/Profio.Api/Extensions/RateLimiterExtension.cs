using System.Threading.RateLimiting;

namespace Profio.Api.Extensions;

public static class RateLimiterExtension
{
  private const string Policy = "PerIp";

  public static void AddRateLimiting(this IServiceCollection services)
  {
    services.AddRateLimiter(options =>
    {
      options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

      options.AddPolicy(Policy, httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
          partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
          factory: _ => new()
          {
            PermitLimit = 60,
            Window = TimeSpan.FromMinutes(1)
          }
        ));
    });
  }

  public static void RequirePerUserRateLimit(this IEndpointConventionBuilder builder)
    => builder.RequireRateLimiting(Policy);
}
