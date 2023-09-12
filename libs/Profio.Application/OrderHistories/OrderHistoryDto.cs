using Profio.Application.Deliveries;
using Profio.Application.Hubs;
using Profio.Domain.Models;

namespace Profio.Application.OrderHistories;

public record OrderHistoryDto : BaseModel
{
  public required string Id { get; set; }
  public DateTime? Timestamp { get; set; }
  public DeliveryDto? Delivery { get; set; }
  public HubDto? Hub { get; set; }
}
