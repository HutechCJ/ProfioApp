using Profio.Domain.Constants;
using Profio.Domain.Models;

namespace Profio.Domain.Views;

public sealed record DeliverySummary(
  Guid Id,
  string? DeliveryId,
  int TotalOrder,
  List<DeliverySummary.DeliveryOrder> Orders) : BaseModel
{
  public sealed record DeliveryOrder(
    string OrderId,
    OrderStatus Status,
    double? Distance
  );
}
