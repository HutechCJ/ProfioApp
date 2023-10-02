using Profio.Domain.Constants;
using Profio.Domain.Interfaces;
using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public sealed class Hub : IEntity
{
  public required string Name { get; set; }
  public required string ZipCode { get; set; }
  public Location? Location { get; set; }
  public Address? Address { get; set; }
  public HubStatus Status { get; set; } = HubStatus.Active;
  public ICollection<Route>? StartRoutes { get; set; } = new List<Route>();
  public ICollection<Route>? EndRoutes { get; set; } = new List<Route>();
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
}
