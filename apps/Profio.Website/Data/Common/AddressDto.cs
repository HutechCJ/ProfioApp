namespace Profio.Website.Data.Common;

public sealed record AddressDto
{
  public string? Street { get; init; }
  public string? Ward { get; init; }
  public string? City { get; init; }
  public string? Province { get; init; }
  public string? ZipCode { get; init; }
}
