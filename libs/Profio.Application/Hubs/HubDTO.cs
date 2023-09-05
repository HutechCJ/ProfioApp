using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Hubs;

public record HubDto : BaseModel
{
  public required string Id { get; set; }
  public required string? ZipCode { get; set; }
  public Location? Location { get; set; }
  public HubStatus Status { get; set; } = HubStatus.Active;
  public ICollection<OrderHistory>? OrderHistories { get; set; } = new List<OrderHistory>();
  public ICollection<Route>? StartRoutes { get; set; } = new List<Route>();
  public ICollection<Route>? EndRoutes { get; set; } = new List<Route>();
}
