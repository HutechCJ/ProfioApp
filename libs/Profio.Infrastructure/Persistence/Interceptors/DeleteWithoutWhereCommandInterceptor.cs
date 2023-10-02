using System.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Profio.Infrastructure.Persistence.Interceptors;

public class DeleteWithoutWhereCommandInterceptor : DbCommandInterceptor
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
    if (command.CommandText.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) &&
        !command.CommandText.Contains("WHERE", StringComparison.OrdinalIgnoreCase))
    {
      Log(command.CommandText, Environment.StackTrace);
    }
  }

  private static void Log(string commandText, string stackTrace)
    => File.AppendAllText("../../../logs.txt", $"DELETE WITHOUT WHERE: {Environment.NewLine} {commandText} {Environment.NewLine} {stackTrace} {Environment.NewLine}");
}
