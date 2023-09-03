using Profio.Domain.Constants;
using Profio.Domain.Models;

namespace Profio.Application.Staffs;

public record StaffDto : BaseModel
{
  public required string Id { get; set; }
  public required string? Name { get; set; }
  public string? Phone { get; set; }
  public Position Position { get; set; }
}
