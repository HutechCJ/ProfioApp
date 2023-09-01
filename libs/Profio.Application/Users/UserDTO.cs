using Profio.Domain.Models;

namespace Profio.Application.Users;

public record UserDTO : BaseModel
{
  public required string Id { get; set; }
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? FullName { get; set; }
}
