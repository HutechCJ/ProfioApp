using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.CQRS.Events.Queries;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.CQRS.Handlers.Queries;

public abstract class GetWithPagingQueryHandler<TQuery, TModel, TEntity> : IRequestHandler<TQuery, ResultModel<IPagedList<TModel>>>
  where TQuery : GetWithPagingQueryBase<TEntity, TModel>
  where TModel : BaseModel
  where TEntity : class, IEntity<object>
{
  private readonly IRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  protected GetWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository<TEntity>(), mapper);

  public async Task<ResultModel<IPagedList<TModel>>> Handle(TQuery request, CancellationToken cancellationToken)
  {
    var query = _repository
      .MultipleResultQuery()
      .Page(request.Criteria.PageNumber, request.Criteria.PageSize);

    //if (request.Criteria.Filter is { })
    //  query = (IMultipleResultQuery<TEntity>)query
    //    .AndFilter(request.Criteria.Filter);

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

    return ResultModel<IPagedList<TModel>>.Create(pagedList);

  }
}
