using Profio.Application.Orders;
using Profio.Domain.Models;
using Profio.Domain.ValueObjects;

namespace Profio.Application.DeliveryProgresses;

public record DeliveryProgressDto : BaseModel
{
  public required string Id { get; init; }
  public Location? CurrentLocation { get; set; }
  public byte PercentComplete { get; set; } = 0;
  public TimeSpan? EstimatedTimeRemaining { get; set; }
  public DateTime? LastUpdated { get; set; }
  public OrderDto? Order { get; set; }
}
