using MediatR;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Abstractions.CQRS.Events.Commands;

public record CreateCommandBase : IRequest<object>, ITxRequest;
