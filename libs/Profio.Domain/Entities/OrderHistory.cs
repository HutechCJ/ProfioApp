using System.Text.Json.Serialization;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public sealed class OrderHistory : IEntity
{
  public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
  public string? DeliveryId { get; set; }
  public Delivery? Delivery { get; set; }
  public string? HubId { get; set; }

  [JsonIgnore] public Hub? Hub { get; set; }

  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
}
