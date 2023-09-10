using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;
using System.Linq.Expressions;

namespace Profio.Application.Abstractions.CQRS.Handlers.Queries;

public abstract class
  GetWithPagingQueryHandler<TQuery, TModel, TEntity> : IRequestHandler<TQuery, IPagedList<TModel>>
  where TQuery : GetWithPagingQueryBase<TEntity, TModel>
  where TModel : BaseModel
  where TEntity : class, IEntity<object>
{
  private readonly IMapper _mapper;
  private readonly IRepository<TEntity> _repository;

  protected GetWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository<TEntity>(), mapper);

  public async Task<IPagedList<TModel>> Handle(TQuery request, CancellationToken cancellationToken)
  {
    var query = (IMultipleResultQuery<TEntity>)_repository
      .MultipleResultQuery()
      .Page(request.Criteria.PageIndex, request.Criteria.PageSize)
      .AndFilter(RequestFilter(request))
      .OrderByDescending(x => x.Id);

    if (request.Criteria.Filter is { })
      query = (IMultipleResultQuery<TEntity>)query
        .AndFilter(Filter(request.Criteria.Filter.ToLowerInvariant()));

    if (request.Criteria.OrderBy is { })
      query = (IMultipleResultQuery<TEntity>)query
        .OrderBy(request.Criteria.OrderBy);

    if (request.Criteria.OrderByDescending is { })
      query = (IMultipleResultQuery<TEntity>)query
        .OrderByDescending(request.Criteria.OrderByDescending);

    var pagedListQueryable = _repository
      .ToQueryable(query);

    var projectedPagedList = pagedListQueryable
      .ProjectTo<TModel>(_mapper.ConfigurationProvider)
      .AsSplitQuery();

    var asyncPagedList = projectedPagedList.ToListAsync(cancellationToken);

    var pagedList = await asyncPagedList
      .Then<List<TModel>, IList<TModel>>(result => result, cancellationToken)
      .ToPagedListAsync(query.Paging.PageIndex,
        query.Paging.PageSize,
        query.Paging.TotalCount,
        cancellationToken);

    return pagedList;
  }
  protected virtual Expression<Func<TEntity, bool>> Filter(string filter) => x => x == null!;
  protected virtual Expression<Func<TEntity, bool>> RequestFilter(TQuery request) => x => true;
}
