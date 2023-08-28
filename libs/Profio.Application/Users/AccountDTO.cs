using Profio.Application.CQRS.Models;

namespace Profio.Application.Users;

public record AccountDTO : BaseModel
{
  public required string Id { get; set; }
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? FullName { get; set; }
  public string? Token { get; set; }
}
