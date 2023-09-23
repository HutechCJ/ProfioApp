namespace Profio.Domain.Contracts;

public class SmsMessage
{
  public string? To { get; set; }
  public string? From { get; set; }
  public string? Message { get; set; }
}
