using MediatR;
using Profio.Domain.Interfaces;

namespace Profio.Application.Abstractions.Idempotency;

public abstract record IdempotentCommand(Guid RequestId) : IRequest, ITxRequest;
