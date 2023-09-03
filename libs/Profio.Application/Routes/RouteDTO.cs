using Profio.Application.Hubs;
using Profio.Domain.Models;

namespace Profio.Application.Routes;

public record RouteDto : BaseModel
{
  public required string Id { get; set; }
  public double? Distance { get; set; }
  public HubDto? StartHub { get; set; }
  public HubDto? EndHub { get; set; }
}
