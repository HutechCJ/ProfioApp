using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Profio.Application.Abstractions.Idempotency;
using Profio.Infrastructure.Logging;
using Profio.Infrastructure.Persistence;
using Profio.Infrastructure.Validator;

namespace Profio.Application.Abstractions.CQRS;

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
        cfg.AddOpenBehavior(typeof(IdempotentCommandBehavior<,>));
      });

    action?.Invoke(services);

    return services;
  }
}
