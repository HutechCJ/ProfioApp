using MediatR;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.CQRS.Events.Commands;

public record DeleteCommandBase<TModel>(object Id) : IRequest<ResultModel<TModel>>, ITxRequest
  where TModel : BaseModel;
