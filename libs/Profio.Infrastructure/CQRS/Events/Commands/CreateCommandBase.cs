using MediatR;
using Profio.Domain.Interfaces;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Events.Commands;

public record CreateCommandBase<T> : IRequest<T>, ITxRequest where T : BaseModel;
