using MediatR;
using Profio.Domain.Interfaces;

namespace Profio.Application.CQRS.Events.Commands;

public record UpdateCommandBase(object Id) : IRequest<Unit>, ITxRequest;
