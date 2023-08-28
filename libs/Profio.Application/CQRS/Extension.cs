using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.Persistence.Relational;
using System.Diagnostics;
using Profio.Infrastructure.Validator;

namespace Profio.Application.CQRS;

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
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>),
          ServiceLifetime.Scoped);
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>),
          ServiceLifetime.Scoped);
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>),
          ServiceLifetime.Scoped);
      });

    action?.Invoke(services);

    return services;
  }
}
