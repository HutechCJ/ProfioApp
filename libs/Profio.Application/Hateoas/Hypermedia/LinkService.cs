using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Profio.Application.Hateoas.Hypermedia;

public sealed class LinkService : ILinkService
{
  private readonly LinkGenerator _linkGenerator;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    => (_linkGenerator, _httpContextAccessor) = (linkGenerator, httpContextAccessor);

  public Link CreateLink(string endPointName, object? routeValues, string? rel, string? method)
    => new(_linkGenerator.GetUriByName(
        _httpContextAccessor.HttpContext ?? throw new InvalidOperationException(),
        endPointName,
        routeValues),
      rel,
      method);
}
