using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Abstractions.CQRS;
using Profio.Infrastructure.Abstractions.Hateoas;

namespace Profio.Application;

public static class ConfigureServices
{
  public static void AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediator(s =>
      s.AddMediatR(options =>
        options.RegisterServicesFromAssembly(AssemblyReference.Assembly)
      ));
    services.AddAutoMapper(AssemblyReference.AppDomainAssembly);
    services.AddValidatorsFromAssemblies(AssemblyReference.AppDomainAssembly);
    services.AddHateoas();
  }

}
