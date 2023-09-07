using Profio.Domain.Constants;
using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public class Customer : BaseEntity
{
  public required string? Name { get; set; }
  public required string? Phone { get; set; }
  public string? Email { get; set; }
  public Gender? Gender { get; set; } = Constants.Gender.Male;
  public required Address? Address { get; set; }
  public ICollection<Order> Orders { get; set; } = new List<Order>();
}
