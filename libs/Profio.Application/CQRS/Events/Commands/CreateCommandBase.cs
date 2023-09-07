using MediatR;
using Profio.Domain.Interfaces;

namespace Profio.Application.CQRS.Events.Commands;

public record CreateCommandBase : IRequest<object>, ITxRequest;
