namespace Profio.Domain.Exceptions;

public sealed class AuthException : Exception
{
  public AuthException()
    : base("You are not authorized to access this resource")
  {
    
  }
}
