using Profio.Domain.Constants;
using Profio.Domain.ValueObjects;

namespace Profio.Domain.Entities;

public class Customer
{
  public required string? Id { get; set; } = Ulid.NewUlid().ToString();
  public required string? Name { get; set; }
  public required string? Phone { get; set; }
  public string? Email { get; set; }
  public Gender? Gender { get; set; } = Constants.Gender.Male;
  public required string? ZipCode { get; set; }
  public required Address? Address { get; set; }
  public ICollection<Order> Orders { get; set; } = new List<Order>();
}
