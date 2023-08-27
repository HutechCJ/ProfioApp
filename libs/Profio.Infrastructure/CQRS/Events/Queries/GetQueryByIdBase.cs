using MediatR;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Events.Queries;

public record GetQueryByIdBase<T>(object Id) : IRequest<ResultModel<T>>
  where T : BaseModel;
