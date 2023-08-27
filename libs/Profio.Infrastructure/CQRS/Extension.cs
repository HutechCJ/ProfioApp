using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Profio.Infrastructure.CQRS;

public static class Extension
{
  [DebuggerStepThrough]
  public static IServiceCollection AddMediator(
    this IServiceCollection services,
    Action<IServiceCollection>? action = null)
  {
    services.AddHttpContextAccessor()
      //.AddMediatR(cfg =>
      //{
      //  cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
      //  cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>),
      //    ServiceLifetime.Scoped);
      //  cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>),
      //    ServiceLifetime.Scoped);
      //})
      ;

    action?.Invoke(services);

    return services;
  }
}
