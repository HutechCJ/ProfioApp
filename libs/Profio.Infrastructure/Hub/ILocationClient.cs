using Profio.Domain.Contracts;

namespace Profio.Infrastructure.Hub;

public interface ILocationClient
{
  public Task SendLocation(VehicleLocation location);
}
