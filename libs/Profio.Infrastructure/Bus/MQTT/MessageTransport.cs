using System.ComponentModel.DataAnnotations;

namespace Profio.Infrastructure.Bus.MQTT;

public class MessageTransport
{
  [Required(ErrorMessage = "MQTT host is required.")]
  [Url(ErrorMessage = "MQTT host is invalid.")]
  public string Host { get; set; } = string.Empty;

  [Required(ErrorMessage = "MQTT username is required.")]
  public string UserName { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;
  public int Mqtt { get; set; } = 8883;
  public int Socket { get; set; } = 8084;
}
