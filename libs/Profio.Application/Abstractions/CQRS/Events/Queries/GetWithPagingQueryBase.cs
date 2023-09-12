using EntityFrameworkCore.Repository.Collections;
using MediatR;
using Profio.Domain.Models;
using Profio.Domain.Specifications;

namespace Profio.Application.Abstractions.CQRS.Events.Queries;

public record GetWithPagingQueryBase<TModel>
  (Criteria Criteria) : IRequest<IPagedList<TModel>>
  where TModel : BaseModel;
