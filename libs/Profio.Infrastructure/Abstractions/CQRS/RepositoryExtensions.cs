using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Models;

namespace Profio.Infrastructure.Abstractions.CQRS;

public static class RepositoryExtensions
{
  public static async Task<IPagedList<TModel>> GetDataWithQueryAsync<TEntity, TModel>(
        this IRepository<TEntity> repository,
        IMultipleResultQuery<TEntity> query,
        IConfigurationProvider configurationProvider, CancellationToken cancellationToken)
    where TEntity : class
    where TModel : BaseModel
  {
    var pagedListQueryable = repository.ToQueryable(query);

    var projectedPagedList = pagedListQueryable
      .ProjectTo<TModel>(configurationProvider)
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
}
