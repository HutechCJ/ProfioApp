using Profio.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Profio.Domain.Entities;

public class Route : IEntity
{
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
  public double? Distance { get; set; }
  public string? StartHubId { get; set; }
  [JsonIgnore]
  public Hub? StartHub { get; set; }
  public string? EndHubId { get; set; }
  [JsonIgnore]
  public Hub? EndHub { get; set; }
}
