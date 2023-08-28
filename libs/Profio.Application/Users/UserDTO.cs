using Profio.Domain.Models;

namespace Profio.Application.Users;

public record UserDTO : BaseModel
{
  public string? Id { get; set; }
  public string? UserName { get; set; }
}
