using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Logging;

namespace Profio.Infrastructure.CQRS;

public static class Extension
{
  [DebuggerStepThrough]
  public static IServiceCollection AddMediator(
    this IServiceCollection services,
    Action<IServiceCollection>? action = null)
  {
    services.AddHttpContextAccessor()
      .AddMediatR(cfg =>
      {
        cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>),
          ServiceLifetime.Scoped);
      });

    action?.Invoke(services);

    return services;
  }
}
