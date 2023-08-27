using MediatR;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Events.Queries;

public record GetByFieldQueryBase<T>(string FieldValue) : IRequest<ListResultModel<T>>
  where T : BaseModel;
