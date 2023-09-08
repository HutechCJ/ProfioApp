using Microsoft.Extensions.DependencyInjection;
using Profio.Application.Abstractions.Hateoas.Hypermedia;

namespace Profio.Application.Abstractions.Hateoas;

public static class Extension
{
  public static IServiceCollection AddHateoas(this IServiceCollection services)
  {
    services.AddScoped<ILinkService, LinkService>();
    services.AddHttpContextAccessor();

    return services;
  }
}
