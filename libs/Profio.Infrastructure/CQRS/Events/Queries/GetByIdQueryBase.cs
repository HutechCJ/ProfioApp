using MediatR;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Events.Queries;

public record GetByIdQueryBase<T>(object Id) : IRequest<ResultModel<T>>
  where T : BaseModel;
