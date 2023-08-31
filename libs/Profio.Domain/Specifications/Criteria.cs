using System.Linq.Expressions;
using Profio.Domain.Models;

namespace Profio.Domain.Specifications;

public class Criteria<T> where T : BaseModel
{
  public Expression<Func<T, bool>>? Filter = null;
  public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null;
  public string? IncludeProperties = "";
  public int Skip = 0;
  public int Take = 0;
  public int PageNumber = 1;
  public int PageSize = 10;
}
