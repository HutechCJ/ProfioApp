using System.Linq.Expressions;
using Profio.Domain.Specifications;

namespace Profio.Domain.Interfaces;

public interface IRelationalRepository<T> where T : class
{
  public Task AddAsync(T entity, CancellationToken cancellationToken = default);
  public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
  public Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
  public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
  public Task<IEnumerable<T>> GetByAsync(
    Expression<Func<T, bool>> predicate,
    CancellationToken cancellationToken = default);
  public IQueryable<T> GetPaging(ISpecification<T> specification);
  public IEnumerable<T> GetShaping(IEnumerable<T> entities, string fieldsString);
  public Task<int> CountAsync(CancellationToken cancellationToken = default);
  public Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
