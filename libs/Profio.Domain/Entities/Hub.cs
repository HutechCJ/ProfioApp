using Profio.Domain.Constants;
using Profio.Domain.Interfaces;
using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public class Hub : IEntity<string>
{
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
  public required string? Name { get; set; }
  public required string? ZipCode { get; set; }
  public Location? Location { get; set; }
  public Address? Address { get; set; }
  public HubStatus Status { get; set; } = HubStatus.Active;
  public ICollection<OrderHistory>? OrderHistories { get; set; } = new List<OrderHistory>();
  public ICollection<Route>? StartRoutes { get; set; } = new List<Route>();
  public ICollection<Route>? EndRoutes { get; set; } = new List<Route>();
}
