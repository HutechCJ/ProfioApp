using System.Text.Json.Serialization;
using Profio.Domain.Constants;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public sealed class Incident : IEntity
{
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; } = IncidentStatus.InProgress;
  public DateTime? Time { get; set; }
  public string? DeliveryId { get; set; }
  [JsonIgnore]
  public Delivery? Delivery { get; set; }
}
