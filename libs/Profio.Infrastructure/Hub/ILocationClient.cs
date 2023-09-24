using Profio.Domain.Contracts;

namespace Profio.Infrastructure.Hub;

public interface ILocationClient
{
  Task SendLocation(VehicleLocation location);
}
