namespace Profio.Infrastructure.Hub;

public interface ILocationClient
{
  Task SendLocation(string message);
}
