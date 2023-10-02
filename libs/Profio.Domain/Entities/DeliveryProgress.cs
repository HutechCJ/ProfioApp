using System.Text.Json.Serialization;
using Profio.Domain.Interfaces;
using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public sealed class DeliveryProgress : IEntity
{
  public Location? CurrentLocation { get; set; }
  public byte PercentComplete { get; set; } = 0;
  public TimeSpan? EstimatedTimeRemaining { get; set; }
  public DateTime? LastUpdated { get; set; }
  public string? OrderId { get; set; }

  [JsonIgnore] public Order? Order { get; set; }

  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
}
