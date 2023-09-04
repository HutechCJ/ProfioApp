using Hangfire.Dashboard;

namespace Profio.Infrastructure.Jobs;

public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
{
  public bool Authorize(DashboardContext context)
  {
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
      return true;

    var cookies = context.GetHttpContext().Request.Cookies;
    return cookies.TryGetValue("USER-TOKEN", out _);
  }
}
