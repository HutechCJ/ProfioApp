using System.Text.Json.Serialization;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public sealed class Phase : IEntity
{
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
  public int Order { get; set; }
  public string? RouteId { get; set; }
  [JsonIgnore]
  public Route? Route { get; set; }
  public ICollection<Order> Orders { get; set; } = new List<Order>();
}
