namespace Profio.Domain.Contracts;

public sealed class SmsMessage
{
  public string? To { get; set; }
  public string? Message { get; set; }
}
