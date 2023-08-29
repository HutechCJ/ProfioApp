using Profio.Domain.Constants;

namespace Profio.Domain.Entities;

public class Staff
{
  public required string? Id { get; set; } = Ulid.NewUlid().ToString();
  public required string? Name { get; set; }
  public string? Phone { get; set; }
  public Position Position { get; set; } = Position.Driver;
  public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
