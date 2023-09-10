using Profio.Domain.Constants;
using System.Text.Json.Serialization;

namespace Profio.Application.Orders;

public class OrderEnumFilter
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public OrderStatus Status { get; set; }
}
