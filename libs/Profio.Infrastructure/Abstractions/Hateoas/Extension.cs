using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Abstractions.Hateoas.Hypermedia;

namespace Profio.Infrastructure.Abstractions.Hateoas;

public static class Extension
{
  public static IServiceCollection AddHateoas(this IServiceCollection services)
  {
    services.AddScoped<ILinkService, LinkService>();
    services.AddHttpContextAccessor();

    return services;
  }
}
