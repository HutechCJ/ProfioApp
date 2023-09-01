using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public class Route : IEntity<string>
{
  public required string Id { get; set; } = Ulid.NewUlid().ToString();
  public double? Distance { get; set; }
  public string? StartHubId { get; set; }
  public Hub? StartHub { get; set; }
  public string? EndHubId { get; set; }
  public Hub? EndHub { get; set; }
}
