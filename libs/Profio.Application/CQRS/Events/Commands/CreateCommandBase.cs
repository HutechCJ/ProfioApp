using MediatR;
using Profio.Application.CQRS.Models;
using Profio.Domain.Interfaces;

namespace Profio.Application.CQRS.Events.Commands;

public record CreateCommandBase<T> : IRequest<T>, ITxRequest where T : BaseModel;
