using Hangfire.Dashboard;

namespace Profio.Infrastructure.Jobs;

public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
{
  public bool Authorize(DashboardContext context) => true;
}
