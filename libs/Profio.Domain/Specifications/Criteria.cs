using Profio.Domain.Interfaces;
using System.Linq.Expressions;

namespace Profio.Domain.Specifications;

public class Criteria<T> where T : IEntity<object>
{
  public Expression<Func<T, bool>>? Filter = null;
  public Expression<Func<T, object>>? OrderBy { get; }
  public Expression<Func<T, object>>? OrderByDescending { get; }
  public List<string> IncludeStrings { get; } = new List<string>();
  public int Skip = 0;
  public int Take = 0;
  public int PageNumber = 1;
  public int PageSize = 10;
}
