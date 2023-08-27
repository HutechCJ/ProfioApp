using MediatR;
using Profio.Application.CQRS.Models;
using Profio.Domain.Interfaces;

namespace Profio.Application.CQRS.Events.Commands;

public record UpdateCommandBase<T>(object Id) : IRequest<T>, ITxRequest
  where T : BaseModel;
