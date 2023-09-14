namespace Profio.Infrastructure.Exceptions;

public sealed class SmsException : Exception
{
  public SmsException(object code, object message)
    : base($"SpeedSms has error code {code} with message {message}")
  {
  }
}
