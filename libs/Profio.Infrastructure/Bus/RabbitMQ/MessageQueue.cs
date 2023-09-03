namespace Profio.Infrastructure.Bus.RabbitMQ;

public class MessageQueue
{
  public string Host { get; set; } = string.Empty;
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
}
