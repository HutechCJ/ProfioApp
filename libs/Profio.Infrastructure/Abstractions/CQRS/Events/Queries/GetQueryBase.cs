using MediatR;
using Profio.Domain.Models;

namespace Profio.Infrastructure.Abstractions.CQRS.Events.Queries;

public record GetQueryBase<T> : IRequest<ListResultModel<T>>
  where T : BaseModel;
