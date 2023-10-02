namespace Profio.Infrastructure.Abstractions.Hateoas.Hypermedia;

public interface ILinkService
{
  public Link CreateLink(string endPointName, object? routeValues, string? rel, string? method);
}
