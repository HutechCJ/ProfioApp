using System.Text.Json.Serialization;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public sealed class Route : IEntity
{
  public double? Distance { get; set; }
  public string? StartHubId { get; set; }
  public string? EndHubId { get; set; }

  [JsonIgnore] public Hub? StartHub { get; set; }

  [JsonIgnore] public Hub? EndHub { get; set; }

  public ICollection<Phase>? Phases { get; set; } = new List<Phase>();
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
}
