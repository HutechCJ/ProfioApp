using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;

namespace Profio.Infrastructure.Persistence.Interceptors;

public class TimingInterceptor : DbCommandInterceptor
{
  private readonly Stopwatch _stopwatch = new();
  private const long MaxAllowedExecutionTime = 5000;

  public override InterceptionResult<DbDataReader> ReaderExecuting(
    DbCommand command,
    CommandEventData eventData,
    InterceptionResult<DbDataReader> result)
  {
    _stopwatch.Restart();
    return base.ReaderExecuting(command, eventData, result);
  }

  public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
    DbCommand command,
    CommandEventData eventData,
    InterceptionResult<DbDataReader> result,
    CancellationToken cancellationToken = default)
  {
    _stopwatch.Stop();
    var executionTime = _stopwatch.ElapsedMilliseconds;

    if (executionTime <= MaxAllowedExecutionTime)
      return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);

    var stackTrace = string.Join("\n", Environment.StackTrace.Split('\n').Select(x => x));
    var message = $"[WARNING] {command.CommandText} took {executionTime}ms to execute. " +
                  $"This is longer than the maximum allowed execution time of {MaxAllowedExecutionTime}ms. " +
                  $"This query should be optimized or split into smaller queries. " +
                  $"Stack trace: {stackTrace}";
    File.AppendAllText("../../../logs.txt", message);

    return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
  }
}
