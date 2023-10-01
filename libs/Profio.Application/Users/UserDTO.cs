using Profio.Application.Staffs;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users;

[SwaggerSchema(
  Title = "User",
  Description = "A Representation of list of User")]
public sealed record UserDto : BaseModel
{
  public required string Id { get; set; }
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? Image { get; set; }
  public StaffDto? Staff { get; set; }
  public string? FullName { get; set; }
}
