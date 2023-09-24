using Profio.Domain.Constants;
using Profio.Website.Data.Common.Dtos;

namespace Profio.Website.Data.Orders;

public sealed record OrderDto
{
  public required string Id { get; init; }
  public DateTime StartedDate { get; init; }
  public DateTime? ExpectedDeliveryTime { get; init; }
  public OrderStatus Status { get; init; }
  public AddressDto? DestinationAddress { get; init; }
  public required string? DestinationZipCode { get; init; }
  public string? Note { get; init; }
  public double? Distance { get; init; }
}
