namespace Profio.Infrastructure.Bus.MQTT;

public class MessageTransport
{
  public string Host { get; set; } = string.Empty;
  public string UserName { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public int Mqtt { get; set; } = 8883;
  public int Socket { get; set; } = 8084;
}
