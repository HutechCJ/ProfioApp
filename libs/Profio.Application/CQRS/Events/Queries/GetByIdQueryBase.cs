using MediatR;
using Profio.Application.CQRS.Models;

namespace Profio.Application.CQRS.Events.Queries;

public record GetByIdQueryBase<T>(object Id) : IRequest<ResultModel<T>>
  where T : BaseModel;
