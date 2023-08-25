using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Profio.Application;

public static class ConfigureServices
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    //services.AddAutoMapper(Assembly.GetExecutingAssembly());
    //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    services.AddMediatR(config =>
    {
      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      //config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    });
    //services.AddMemoryCache();
    return services;
  }
}
