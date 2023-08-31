using EntityFrameworkCore.Repository.Collections;
using MediatR;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;
using Profio.Domain.Specifications;

namespace Profio.Application.CQRS.Events.Queries;

public record GetWithPagingQueryBase<TEntity, TModel>(Criteria<TEntity> Criteria) : IRequest<IPagedList<TModel>>
  where TModel : BaseModel
  where TEntity : IEntity<object>;
