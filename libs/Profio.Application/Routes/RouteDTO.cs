using Profio.Application.Hubs;
using Profio.Domain.Models;

namespace Profio.Application.Routes;

public record RouteDTO : BaseModel
{
  public required string Id { get; set; }
  public double? Distance { get; set; }
  public HubDTO? StartHub { get; set; }
  public HubDTO? EndHub { get; set; }
}
