namespace Profio.Domain.Contracts;

public sealed record MessageRequest
{
  public string? Title { get; set; }
  public string? Body { get; set; }
  public string? DeviceToken { get; set; }
}
