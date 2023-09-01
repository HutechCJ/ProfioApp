using Hangfire.Dashboard;

namespace Profio.Infrastructure.Jobs;

public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
{
  public bool Authorize(DashboardContext context)
  {
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
      return true;

    var httpContext = context.GetHttpContext();
    return httpContext.User.Identity?.IsAuthenticated ?? false;
  }
}
