using Profio.Domain.Constants;
using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public class Order : BaseEntity
{
  public DateTime StartedDate { get; set; } = DateTime.UtcNow;
  public DateTime? ExpectedDeliveryTime { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Pending;
  public Address? DestinationAddress { get; set; }
  public required string DestinationZipCode { get; set; }
  public string? Note { get; set; }
  public double? Distance { get; set; }
  public string? CustomerId { get; set; }
  public Customer? Customer { get; set; }
  public ICollection<Delivery>? Deliveries { get; set; } = new List<Delivery>();
  public ICollection<DeliveryProgress> DeliveryProgresses { get; set; } = new List<DeliveryProgress>();
}
