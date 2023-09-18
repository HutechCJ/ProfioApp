using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Entities;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Identity;

public class ApplicationUser : IdentityUser, IEntity<string>
{
  [PersonalData]
  public virtual string? FullName { get; set; }
  [PersonalData]
  public virtual string? ImageUrl { get; set; }
  [PersonalData]
  public virtual string? StaffId { get; set; }
  [JsonIgnore]
  public Staff? Staff { get; set; }
}
