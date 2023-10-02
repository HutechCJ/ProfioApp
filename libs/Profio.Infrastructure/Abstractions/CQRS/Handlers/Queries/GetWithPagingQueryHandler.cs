using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;

namespace Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;

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
    ArgumentNullException.ThrowIfNull(request, nameof(request));

    var query = (IMultipleResultQuery<TEntity>)_repository
      .MultipleResultQuery()
      .ApplyCriteria(request.Specification)
      .AndFilter(RequestFilter(request));

    if (request.Specification.Filter is { })
      query = (IMultipleResultQuery<TEntity>)query
        .AndFilter(Filter(request.Specification.Filter.ToLowerInvariant()));

    var pagedList = await _repository
      .GetDataWithQueryAsync<TEntity, TModel>(query, _mapper.ConfigurationProvider, cancellationToken);

    return pagedList;
  }

  protected virtual Expression<Func<TEntity, bool>> Filter(string filter) => x => x == null!;
  protected virtual Expression<Func<TEntity, bool>> RequestFilter(TQuery request) => x => true;
}
