using Profio.Application.Routes;
using Profio.Domain.Models;

namespace Profio.Application.Phases;

public sealed record PhaseDto : BaseModel
{
  public required string Id { get; set; } = string.Empty;
  public int Order { get; set; }
  public RouteDto? Route { get; set; }
}
