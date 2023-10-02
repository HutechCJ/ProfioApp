using System.Text.Json.Serialization;
using Profio.Domain.Constants;
using Profio.Domain.Interfaces;
using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public sealed class Order : IEntity
{
  public DateTime StartedDate { get; set; } = DateTime.UtcNow;
  public DateTime? ExpectedDeliveryTime { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Pending;
  public Address? DestinationAddress { get; set; }
  public required string DestinationZipCode { get; set; }
  public string? Note { get; set; }
  public double? Distance { get; set; }
  public string? CustomerId { get; set; }
  public string? PhaseId { get; set; }

  [JsonIgnore] public Phase? Phase { get; set; }

  [JsonIgnore] public Customer? Customer { get; set; }

  public ICollection<Delivery>? Deliveries { get; set; } = new List<Delivery>();
  public ICollection<DeliveryProgress> DeliveryProgresses { get; set; } = new List<DeliveryProgress>();
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
}
