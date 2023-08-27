using MediatR;
using Profio.Domain.Interfaces;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Events.Commands;

public record DeleteCommandBase<T>(object Id) : IRequest<T>, ITxRequest
  where T : BaseModel;
