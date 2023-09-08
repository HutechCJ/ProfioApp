using MediatR;
using Profio.Domain.Interfaces;

namespace Profio.Application.Abstractions.CQRS.Events.Commands;

public record UpdateCommandBase(object Id) : IRequest<Unit>, ITxRequest;
