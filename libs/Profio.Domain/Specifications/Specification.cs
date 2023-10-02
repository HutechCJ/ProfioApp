namespace Profio.Domain.Specifications;

public sealed class Specification
{
  public string? Filter { get; set; }
  public string? OrderBy { get; set; }
  public string? OrderByDescending { get; set; }
  public List<string> IncludeStrings { get; } = new();
  public int PageIndex { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}
