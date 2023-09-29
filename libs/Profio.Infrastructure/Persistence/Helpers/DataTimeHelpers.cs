using System.Diagnostics;

namespace Profio.Infrastructure.Persistence.Helpers;

public static class DateTimeHelper
{
  [DebuggerStepThrough]
  public static DateTime NewDateTime()
    => ToDateTime(DateTimeOffset.Now.UtcDateTime);

  public static DateTime ToDateTime(this DateTime datetime)
    => DateTime.SpecifyKind(datetime, DateTimeKind.Utc);
}
