using MediatR;
using Profio.Domain.Models;

namespace Profio.Infrastructure.Abstractions.CQRS.Events.Queries;

public record GetByFieldQueryBase<T>(string FieldValue) : IRequest<ListResultModel<T>>
  where T : BaseModel;
