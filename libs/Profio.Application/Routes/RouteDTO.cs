using Profio.Application.Hubs;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Routes;

[SwaggerSchema(
  Title = "Route",
  Description = "A Representation of Route")]
public record RouteDto : BaseModel
{
  public required string Id { get; set; }
  public double? Distance { get; set; }
  public HubDto? StartHub { get; set; }
  public HubDto? EndHub { get; set; }
}
