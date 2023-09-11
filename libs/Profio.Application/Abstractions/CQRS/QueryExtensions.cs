using EntityFrameworkCore.QueryBuilder.Interfaces;
using Profio.Domain.Interfaces;
using Profio.Domain.Specifications;

namespace Profio.Application.Abstractions.CQRS;

public static class QueryExtensions
{
  public static IMultipleResultQuery<TEntity> ApplyCriteria<TEntity>(
        this IMultipleResultQuery<TEntity> query,
        Criteria criteria)
    where TEntity : class, IEntity<object>
  {
    query = (IMultipleResultQuery<TEntity>)query
      .Page(criteria.PageIndex, criteria.PageSize)
      .OrderByDescending(x => x.Id);
    if (criteria.OrderBy is { })
      query = (IMultipleResultQuery<TEntity>)query
        .OrderBy(criteria.OrderBy);

    if (criteria.OrderByDescending is { })
      query = (IMultipleResultQuery<TEntity>)query
        .OrderByDescending(criteria.OrderByDescending);

    return query;
  }
}
