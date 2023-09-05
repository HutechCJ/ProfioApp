using Profio.Domain.Interfaces;

namespace Profio.Domain.Specifications;

public class Criteria<T> where T : IEntity<object>
{
  public string? Filter { get; set; }
  public string? OrderBy { get; set; }
  public string? OrderByDescending { get; set; }
  public List<string> IncludeStrings { get; } = new List<string>();
  public int Skip { get; set; } = 0;
  public int Take { get; set; } = 0;
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}
