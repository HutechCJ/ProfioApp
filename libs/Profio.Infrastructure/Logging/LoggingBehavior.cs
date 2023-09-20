using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Profio.Infrastructure.Logging;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : notnull
{
  private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

  public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    => _logger = logger;

  public async Task<TResponse> Handle(TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  {
    const string prefix = nameof(LoggingBehavior<TRequest, TResponse>);

    _logger.LogInformation("[{Prefix}] Handling {X-RequestData}", prefix, typeof(TRequest).Name);

    var timer = new Stopwatch();

    timer.Start();

    var response = await next();

    timer.Stop();

    var timeTaken = timer.Elapsed;

    if (timeTaken.Seconds > 3)
      _logger.LogWarning("[{Perf-Possible}] The request {X-RequestData} took {TimeTaken} seconds.",
        prefix, typeof(TRequest).Name, timeTaken.Seconds);

    _logger.LogInformation("[{Prefix}] Handled {X-RequestData}", prefix, typeof(TRequest).Name);
    return response;
  }
}
