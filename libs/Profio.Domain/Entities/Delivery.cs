using System.Text.Json.Serialization;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public sealed class Delivery : IEntity
{
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
  public DateTime? DeliveryDate { get; set; } = DateTime.UtcNow;
  public string? OrderId { get; set; }
  public Order? Order { get; set; }
  public string? VehicleId { get; set; }
  [JsonIgnore]
  public Vehicle? Vehicle { get; set; }
  public ICollection<Incident>? Incidents { get; set; } = new List<Incident>();
}
