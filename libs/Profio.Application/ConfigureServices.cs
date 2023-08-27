using Microsoft.Extensions.DependencyInjection;

namespace Profio.Application;

public static class ConfigureServices
{
  public static void AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(config =>
      config.RegisterServicesFromAssembly(AssemblyReference.Assembly)
    );
  }
}
