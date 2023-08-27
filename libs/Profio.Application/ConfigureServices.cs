using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Profio.Application;

public static class ConfigureServices
{
  public static void AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(config =>
      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
    );
  }
}
