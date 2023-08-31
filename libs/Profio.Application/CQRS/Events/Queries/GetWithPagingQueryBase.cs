using EntityFrameworkCore.Repository.Collections;
using MediatR;
using Profio.Domain.Models;
using Profio.Domain.Specifications;

namespace Profio.Application.CQRS.Events.Queries;

public record GetWithPagingQueryBase<T>(Criteria<T> Criteria) : IRequest<IPagedList<T>>
  where T : BaseModel;
