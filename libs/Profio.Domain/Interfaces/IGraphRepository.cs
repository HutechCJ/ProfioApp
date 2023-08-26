using System.Linq.Expressions;

namespace Profio.Domain.Interfaces;

public interface IGraphRepository<T> where T : class
{
  Task<List<T>> GetAsync(string query);
  Task<List<T>> GetByParam(string query, Dictionary<string, object> parameters);
  Task<T> GetOne(string query, Expression<Func<T, bool>> predicate);
  Task CreateAsync(string query, T node);
  Task UpdateAsync(string query, T node);
  Task DeleteAsync(string query);
}
