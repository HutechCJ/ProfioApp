using Profio.Application.Customers;
using Profio.Application.Phases;
using Profio.Domain.Constants;
using Profio.Domain.Models;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Orders;

[SwaggerSchema(
  Title = "Order",
  Description = "A Representation of list of Order")]
public sealed record OrderDto : BaseModel
{
  public required string Id { get; init; }
  public DateTime StartedDate { get; init; }
  public DateTime? ExpectedDeliveryTime { get; init; }
  public OrderStatus Status { get; init; }
  public Address? DestinationAddress { get; init; }
  public required string? DestinationZipCode { get; init; }
  public string? Note { get; init; }
  public double? Distance { get; init; }
  public CustomerDto? Customer { get; init; }
  public PhaseDto? Phase { get; init; }
}
