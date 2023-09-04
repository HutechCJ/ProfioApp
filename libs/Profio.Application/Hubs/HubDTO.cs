using Profio.Domain.Constants;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Hubs;

[SwaggerSchema(
  Title = "Hub",
  Description = "A Representation of Hub")]
public class HubDto
{
  public required string? Name { get; set; }
  public required string? ZipCode { get; set; }
  public Location? Location { get; set; }
  public Address? Address { get; set; }
  public HubStatus Status { get; set; } = HubStatus.Active;
}
