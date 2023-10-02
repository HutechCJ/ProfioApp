using Profio.Domain.Constants;
using Profio.Domain.Models;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Hubs;

[SwaggerSchema(
  Title = "Hub",
  Description = "A Representation of list of Hub")]
public sealed record HubDto : BaseModel
{
  public required string Id { get; set; }
  public required string? ZipCode { get; set; }
  public Location? Location { get; set; }
  public HubStatus Status { get; set; } = HubStatus.Active;
}
