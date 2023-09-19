using Profio.Application.Orders;
using Profio.Application.Vehicles;
using Profio.Domain.Models;

namespace Profio.Application.Deliveries;

public sealed record DeliveryDto : BaseModel
{
  public required string Id { get; set; }
  public DateTime? DeliveryDate { get; set; }
  public OrderDto? Order { get; set; }
  public VehicleDto? Vehicle { get; set; }
}
