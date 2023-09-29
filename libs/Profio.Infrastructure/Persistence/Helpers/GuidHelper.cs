using System.Diagnostics;

namespace Profio.Infrastructure.Persistence.Helpers;

public static class GuidHelper
{
  [DebuggerStepThrough]
  public static Guid NewGuid(string? guid = default)
    =>string.IsNullOrWhiteSpace(guid) ? Guid.NewGuid() : new(guid);
}
