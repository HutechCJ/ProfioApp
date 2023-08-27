using Microsoft.Extensions.DependencyInjection;
using Profio.Application.CQRS;

namespace Profio.Application;

public static class ConfigureServices
{
  public static void AddApplicationServices(this IServiceCollection services)
  {
    //services.AddMediatR(config =>
    //{
    //  config.RegisterServicesFromAssembly(AssemblyReference.ExecuteAssembly);
    //});
    services.AddMediator();
    services.AddAutoMapper(AssemblyReference.AppDomainAssembly);

  }
}
