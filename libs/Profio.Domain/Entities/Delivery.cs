using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public class Delivery : IEntity<string>
{
  public string Id { get; set; } = Ulid.NewUlid().ToString();
  public DateTime? DeliveryDate { get; set; } = DateTime.UtcNow;
  public string? OrderId { get; set; }
  public Order? Order { get; set; }
  public string? VehicleId { get; set; }
  public Vehicle? Vehicle { get; set; }
  public ICollection<OrderHistory>? OrderHistories { get; set; } = new List<OrderHistory>();
}
