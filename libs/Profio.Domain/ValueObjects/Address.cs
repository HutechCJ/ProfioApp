using Microsoft.EntityFrameworkCore;
using Profio.Domain.Primitives;

namespace Profio.Domain.ValueObjects;

[Owned]
public sealed class Address : ValueObject
{
  public Address()
  {
  }

  public Address(
    string? street,
    string? ward,
    string? city,
    string? province,
    string? zipCode)
  {
    Street = street;
    Ward = ward;
    City = city;
    Province = province;
    ZipCode = zipCode;
  }

  public string? Street { get; set; }
  public string? Ward { get; set; }
  public string? City { get; set; }
  public string? Province { get; set; }
  public string? ZipCode { get; set; }

  public override string ToString()
  {
    var components = new List<string?>
    {
      Street, Ward, City, Province, ZipCode
    };

    var nonEmptyComponents = components
      .Where(c => !string.IsNullOrWhiteSpace(c))
      .ToList();

    return string.Join(", ", nonEmptyComponents);
  }

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return ToString();
  }
}
