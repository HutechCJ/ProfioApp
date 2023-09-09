using MediatR;

namespace Profio.Application.Abstractions.Idempotency;

public abstract record IdempotentCommand(Guid RequestId) : IRequest;
