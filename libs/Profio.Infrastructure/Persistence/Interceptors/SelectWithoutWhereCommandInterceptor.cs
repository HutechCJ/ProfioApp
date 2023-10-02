using System.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Profio.Infrastructure.Persistence.Interceptors;

public sealed class SelectWithoutWhereCommandInterceptor : DbCommandInterceptor
{
  public override InterceptionResult<DbDataReader> ReaderExecuting(
    DbCommand command,
    CommandEventData eventData,
    InterceptionResult<DbDataReader> result)
  {
    CheckCommand(command);
    return result;
  }

  public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
    DbCommand command,
    CommandEventData eventData,
    InterceptionResult<DbDataReader> result,
    CancellationToken cancellationToken = default)
  {
    CheckCommand(command);
    return new(result);
  }

  private static void CheckCommand(IDbCommand command)
  {
    if (command.CommandText.Contains("SELECT COUNT(*)", StringComparison.OrdinalIgnoreCase))
      return;

    if (!command.CommandText.Contains("SELECT", StringComparison.OrdinalIgnoreCase))
      return;

    if (command.CommandText.Contains("WHERE", StringComparison.OrdinalIgnoreCase))
      return;

    if (command.CommandText.Contains("OFFSET", StringComparison.OrdinalIgnoreCase))
      return;

    if (command.CommandText.Contains("FETCH", StringComparison.OrdinalIgnoreCase))
      return;

    var stackTrace = string.Join("\n", Environment.StackTrace.Split('\n')
      .Select(x => x));

    Log(command.CommandText, stackTrace);
  }

  private static void Log(string commandText, string stackTrace)
    => Serilog.Log.Information("SelectWithoutWhereCommandInterceptor: {CommandText}\n{StackTrace}", commandText, stackTrace);
}
