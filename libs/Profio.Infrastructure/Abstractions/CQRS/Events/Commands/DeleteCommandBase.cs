using MediatR;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Infrastructure.Abstractions.CQRS.Events.Commands;

public record DeleteCommandBase<TModel>(object Id) : IRequest<TModel>, ITxRequest
  where TModel : BaseModel;
