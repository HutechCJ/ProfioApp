using MediatR;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.CQRS.Events.Commands;

public record DeleteCommandBase<T>(object Id) : IRequest<T>, ITxRequest
  where T : BaseModel;
