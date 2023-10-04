using Profio.Infrastructure.Abstractions.Hypermedia;

namespace Profio.Infrastructure.Abstractions.Hypermedia;

public interface ILinkService
{
  public Link CreateLink(string endPointName, object? routeValues, string? rel, string? method);
}
