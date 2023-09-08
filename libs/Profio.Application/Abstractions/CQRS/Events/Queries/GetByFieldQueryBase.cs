using MediatR;
using Profio.Domain.Models;

namespace Profio.Application.Abstractions.CQRS.Events.Queries;

public record GetByFieldQueryBase<T>(string FieldValue) : IRequest<ListResultModel<T>>
  where T : BaseModel;
