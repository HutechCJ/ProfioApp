using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public class OrderHistory : IEntity
{
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
  public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
  public string? DeliveryId { get; set; }
  public Delivery? Delivery { get; set; }
  public string? HubId { get; set; }
  public Hub? Hub { get; set; }
  public ICollection<Incident>? Incidents { get; set; } = new List<Incident>();
}
