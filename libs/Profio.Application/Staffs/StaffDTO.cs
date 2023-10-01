using Profio.Domain.Constants;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Staffs;

[SwaggerSchema(
  Title = "Staff",
  Description = "A Representation of Staff")]
public sealed record StaffDto : BaseModel
{
  public required string Id { get; set; }
  public required string? Name { get; set; }
  public string? Phone { get; set; }
  public Position Position { get; set; }
}
