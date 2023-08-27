using MediatR;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Events.Queries;

public record GetQueryBase<T> : IRequest<ListResultModel<T>>
  where T : BaseModel;
