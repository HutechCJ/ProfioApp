using Profio.Application.Routes;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Phases;

[SwaggerSchema(
  Title = "Phase",
  Description = "A Representation of list of Phase")]
public sealed record PhaseDto : BaseModel
{
  public required string Id { get; set; } = string.Empty;
  public int Order { get; set; }
  public RouteDto? Route { get; set; }
}
