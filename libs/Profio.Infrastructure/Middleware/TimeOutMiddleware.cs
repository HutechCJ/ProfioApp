using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;

namespace Profio.Infrastructure.Middleware;

public sealed class TimeOutMiddleware
{
  private readonly ILogger<TimeOutMiddleware> _logger;
  private readonly RequestDelegate _next;

  public TimeOutMiddleware(RequestDelegate next, ILogger<TimeOutMiddleware> logger)
    => (_next, _logger) = (next, logger);

  public async Task InvokeAsync(HttpContext httpContext)
  {
    var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromMinutes(2), async (_, _, _) =>
    {
      _logger.LogError("Request {Path} timed out", httpContext.Request.Path);
      httpContext.Response.StatusCode = StatusCodes.Status408RequestTimeout;
      await httpContext.Response.WriteAsync("Request timed out");
    });

    await timeoutPolicy.ExecuteAsync(async () =>
    {
      _logger.LogInformation("Executing request for {Path}", httpContext.Request.Path);
      await _next(httpContext);
    });
  }
}
