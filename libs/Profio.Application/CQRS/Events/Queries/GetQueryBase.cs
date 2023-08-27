using MediatR;
using Profio.Application.CQRS.Models;

namespace Profio.Application.CQRS.Events.Queries;

public record GetQueryBase<T> : IRequest<ListResultModel<T>>
  where T : BaseModel;
