using System.Net.Mime;

namespace Profio.Website.Middleware;

public sealed class RobotsTxtMiddleware
{
  private readonly RequestDelegate _next;

  public RobotsTxtMiddleware(RequestDelegate next)
    => _next = next;

  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Path == "/robots.txt")
    {
      const string robotsTxtContent = "User-agent: *\nDisallow: /";

      context.Response.ContentType = MediaTypeNames.Text.Plain;
      await context.Response.WriteAsync(robotsTxtContent);
    }
    else
    {
      await _next(context);
    }
  }
}

public static class RobotsTxtMiddlewareExtensions
{
  public static IApplicationBuilder UseRobotsTxtMiddleware(this IApplicationBuilder builder)
    => builder.UseMiddleware<RobotsTxtMiddleware>();
}
