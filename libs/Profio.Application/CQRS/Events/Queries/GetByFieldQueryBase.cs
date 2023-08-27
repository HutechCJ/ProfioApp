using MediatR;
using Profio.Application.CQRS.Models;

namespace Profio.Application.CQRS.Events.Queries;

public record GetByFieldQueryBase<T>(string FieldValue) : IRequest<ListResultModel<T>>
  where T : BaseModel;