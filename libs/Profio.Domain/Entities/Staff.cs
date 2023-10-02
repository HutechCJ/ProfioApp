using Profio.Domain.Constants;
using Profio.Domain.Identity;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public sealed class Staff : IEntity
{
  public required string Name { get; set; }
  public string? Phone { get; set; }
  public Position Position { get; set; } = Position.Driver;
  public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
  public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
}
