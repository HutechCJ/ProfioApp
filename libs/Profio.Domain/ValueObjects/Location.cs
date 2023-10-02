using Microsoft.EntityFrameworkCore;
using Profio.Domain.Primitives;

namespace Profio.Domain.ValueObjects;

[Owned]
public class Location : ValueObject
{
  public Location()
  {
  }

  public Location(double latitude, double longitude)
    => (Latitude, Longitude) = (latitude, longitude);

  public double Latitude { get; set; }
  public double Longitude { get; set; }

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Latitude;
    yield return Longitude;
  }
}
