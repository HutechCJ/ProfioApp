using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Relational;

public static class Extension
{
  public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
  }
}
