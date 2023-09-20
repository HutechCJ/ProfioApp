using MediatR;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Abstractions.Idempotency;

public abstract record IdempotentCommand(Guid RequestId) : IRequest, ITxRequest;
