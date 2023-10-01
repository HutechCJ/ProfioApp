using Profio.Application.Staffs;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users;

[SwaggerSchema(
  Title = "Account",
  Description = "A Representation of Account")]
public sealed record AccountDto : BaseModel
{
  public required string Id { get; set; }
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? FullName { get; set; }
  public string? Image { get; set; }
  public string? Token { get; set; }
  public StaffDto? Staff { get; set; }
  public DateTime? TokenExpire { get; set; }
}
