using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public class DeliveryProgress : BaseEntity
{
  public Location? CurrentLocation { get; set; }
  public byte PercentComplete { get; set; } = 0;
  public TimeSpan? EstimatedTimeRemaining { get; set; }
  public DateTime? LastUpdated { get; set; }
  public string? OrderId { get; set; }
  public Order? Order { get; set; }
}
