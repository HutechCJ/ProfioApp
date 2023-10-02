using EntityFrameworkCore.QueryBuilder.Interfaces;
using Profio.Domain.Interfaces;
using Profio.Domain.Specifications;

namespace Profio.Infrastructure.Abstractions.CQRS;

public static class SpecificationEvaluator
{
  public static IMultipleResultQuery<TEntity> ApplyCriteria<TEntity>(
    this IMultipleResultQuery<TEntity> query,
    Specification specification)
    where TEntity : class, IEntity<object>
  {
    query = (IMultipleResultQuery<TEntity>)query
      .Page(specification.PageIndex, specification.PageSize)
      .OrderByDescending(x => x.Id);

    if (specification.OrderBy is { })
      query = (IMultipleResultQuery<TEntity>)query
        .OrderBy(specification.OrderBy);

    if (specification.OrderByDescending is { })
      query = (IMultipleResultQuery<TEntity>)query
        .OrderByDescending(specification.OrderByDescending);

    return query;
  }
}
