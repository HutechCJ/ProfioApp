using AutoMapper;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;
using System.Linq.Expressions;

namespace Profio.Application.Abstractions.CQRS.Handlers.Queries;

public abstract class
  GetWithPagingQueryHandler<TQuery, TModel, TEntity> : IRequestHandler<TQuery, IPagedList<TModel>>
  where TQuery : GetWithPagingQueryBase<TModel>
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
      .ApplyCriteria(request.Criteria)
      .AndFilter(RequestFilter(request));

    if (request.Criteria.Filter is { })
      query = (IMultipleResultQuery<TEntity>)query
        .AndFilter(Filter(request.Criteria.Filter.ToLowerInvariant()));

    var pagedList = await _repository
      .GetDataWithQueryAsync<TEntity, TModel>(query, _mapper.ConfigurationProvider, cancellationToken);

    return pagedList;
  }
  protected virtual Expression<Func<TEntity, bool>> Filter(string filter) => x => x == null!;
  protected virtual Expression<Func<TEntity, bool>> RequestFilter(TQuery request) => x => true;
}
