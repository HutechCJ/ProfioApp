using Profio.Domain.Constants;

namespace Profio.Domain.Entities;

public class Order
{
  public required string? Id { get; set; } = Ulid.NewUlid().ToString();
  public DateTime StartedDate { get; set; } = DateTime.UtcNow;
  public DateTime? ExpectedDeliveryTime { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Pending;
  public required string? DestinationZipCode { get; set; }
  public double? Distance { get; set; }
  public string? VehicleId { get; set; }
  public string? CustomerId { get; set; }
  public Vehicle? Vehicle { get; set; }
  public Customer? Customer { get; set; }
  public ICollection<DeliveryProgress> DeliveryProgresses { get; set; } = new List<DeliveryProgress>();
  public ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
}
