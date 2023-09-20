using MediatR;
using Profio.Infrastructure.Persistence.Idempotency;

namespace Profio.Infrastructure.Abstractions.Idempotency;

public sealed class IdempotentCommandBehavior<TRequest, TResponse>
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IdempotentCommand
{
  private readonly IIdempotencyService _idempotencyService;

  public IdempotentCommandBehavior(IIdempotencyService idempotencyService)
    => _idempotencyService = idempotencyService;

  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  {
    if (await _idempotencyService.RequestExistsAsync(request.RequestId))
      return default!;

    await _idempotencyService.CreateRequestForCommandAsync(request.RequestId, typeof(TRequest).Name);

    return await next();
  }
}
