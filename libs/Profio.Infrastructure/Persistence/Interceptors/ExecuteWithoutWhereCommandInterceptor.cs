using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Profio.Infrastructure.Persistence.Interceptors;

public class ExecuteWithoutWhereCommandInterceptor : DbCommandInterceptor
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
    if (IsSelectWithoutWhere(command))
      Log($"SELECT WITHOUT WHERE: {Environment.NewLine}{command.CommandText}");

    else if (IsUpdateWithoutWhere(command))
      Log($"UPDATE WITHOUT WHERE: {Environment.NewLine}{command.CommandText}");

    else if (IsDeleteWithoutWhere(command))
      Log($"DELETE WITHOUT WHERE: {Environment.NewLine}{command.CommandText}");
  }

  private static bool IsSelectWithoutWhere(IDbCommand command)
    => command.CommandText.Contains("SELECT", StringComparison.OrdinalIgnoreCase) &&
       !command.CommandText.Contains("WHERE", StringComparison.OrdinalIgnoreCase) &&
       !command.CommandText.Contains("OFFSET", StringComparison.OrdinalIgnoreCase) &&
       !command.CommandText.Contains("FETCH", StringComparison.OrdinalIgnoreCase) &&
       !command.CommandText.Contains("SELECT COUNT(*)", StringComparison.OrdinalIgnoreCase);

  private static bool IsUpdateWithoutWhere(IDbCommand command)
    => command.CommandText.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) &&
       !command.CommandText.Contains("WHERE", StringComparison.OrdinalIgnoreCase);

  private static bool IsDeleteWithoutWhere(IDbCommand command)
    => command.CommandText.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) &&
       !command.CommandText.Contains("WHERE", StringComparison.OrdinalIgnoreCase);

  private static void Log(string logMessage)
  {
    var stackTrace = string.Join("\n", Environment.StackTrace.Split('\n').Select(x => x));
    File.AppendAllText("../../../logs.txt", $"{logMessage}{Environment.NewLine}{stackTrace}{Environment.NewLine}");
  }
}
