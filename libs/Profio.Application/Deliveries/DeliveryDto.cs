using Profio.Application.Orders;
using Profio.Application.Vehicles;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Deliveries;

[SwaggerSchema(
   Title = "Delivery",
    Description = "A Representation of list of Delivery")]
public sealed record DeliveryDto : BaseModel
{
  public required string Id { get; set; }
  public DateTime? DeliveryDate { get; set; }
  public OrderDto? Order { get; set; }
  public VehicleDto? Vehicle { get; set; }
}
