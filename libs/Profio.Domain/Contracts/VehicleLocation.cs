namespace Profio.Domain.Contracts;

public record VehicleLocation
{
  public string? VehicleId { get; set; }
  public double? Latitude { get; set; }
  public double? Longitude { get; set; }
}
