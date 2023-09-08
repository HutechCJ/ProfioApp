namespace Profio.Infrastructure.Exceptions;

public class ConnectDatabaseException : Exception
{
  public ConnectDatabaseException()
    : base("Cannot connect to database. Please check your connection string and try again.")
  {
  }
}
