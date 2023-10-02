using EntityFrameworkCore.Repository.Collections;
using MediatR;
using Profio.Domain.Models;
using Profio.Domain.Specifications;

namespace Profio.Infrastructure.Abstractions.CQRS.Events.Queries;

public record GetWithPagingQueryBase<TModel>
  (Specification Specification) : IRequest<IPagedList<TModel>>
  where TModel : BaseModel;
