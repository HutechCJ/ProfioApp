using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Profio.Application.CQRS;
using Profio.Application.Hateoas;

namespace Profio.Application;

public static class ConfigureServices
{
  public static void AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediator();
    services.AddAutoMapper(AssemblyReference.AppDomainAssembly);
    services.AddValidatorsFromAssemblies(AssemblyReference.AppDomainAssembly);
    services.AddHateoas();
  }
}
