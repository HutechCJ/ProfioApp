using System.Text.Json.Serialization;

namespace Profio.Domain.Entities;

public class Route : BaseEntity
{
  public double? Distance { get; set; }
  public string? StartHubId { get; set; }
  [JsonIgnore]
  public Hub? StartHub { get; set; }
  public string? EndHubId { get; set; }
  [JsonIgnore]
  public Hub? EndHub { get; set; }
}
