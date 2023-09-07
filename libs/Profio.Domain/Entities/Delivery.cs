namespace Profio.Domain.Entities;

public class Delivery : BaseEntity
{
  public DateTime? DeliveryDate { get; set; } = DateTime.UtcNow;
  public string? OrderId { get; set; }
  public Order? Order { get; set; }
  public string? VehicleId { get; set; }
  public Vehicle? Vehicle { get; set; }
  public ICollection<OrderHistory>? OrderHistories { get; set; } = new List<OrderHistory>();
}
