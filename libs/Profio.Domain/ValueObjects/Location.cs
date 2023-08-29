using Profio.Domain.Primitives;

namespace Profio.Domain.ValueObjects;

public class Location : ValueObject
{
  public double Latitude { get; set; }
  public double Longitude { get; set; }

  public Location() { }

  public Location(double latitude, double longitude)
    => (Latitude, Longitude) = (latitude, longitude);

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Latitude;
    yield return Longitude;
  }
}
