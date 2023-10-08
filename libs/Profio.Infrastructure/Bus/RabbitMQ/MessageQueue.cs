using System.ComponentModel.DataAnnotations;

namespace Profio.Infrastructure.Bus.RabbitMQ;

public class MessageQueue
{
  [Required(ErrorMessage = "RabbitMQ host is required.")]
  [Url(ErrorMessage = "RabbitMQ host is invalid.")]
  public string Host { get; set; } = string.Empty;

  [Required(ErrorMessage = "RabbitMQ username is required.")]
  public string Username { get; set; } = string.Empty;

  [Required(ErrorMessage = "RabbitMQ password is required.")]
  public string Password { get; set; } = string.Empty;
}
