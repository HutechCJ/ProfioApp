namespace Profio.Domain.Contracts;

public record VehicleLocation
{
  public string? Id { get; set; }
  public double? Latitude { get; set; }
  public double? Longitude { get; set; }
  public IEnumerable<string> OrderIds { get; set; } = Enumerable.Empty<string>();
}
