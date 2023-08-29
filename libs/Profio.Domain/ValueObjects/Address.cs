using Profio.Domain.Primitives;

namespace Profio.Domain.ValueObjects;

public sealed class Address : ValueObject
{
  public string? Street { get; set; }
  public string? Ward { get; set; }
  public string? City { get; set; }
  public string? Province { get; set; }
  public string? ZipCode { get; set; }

  public Address() { }

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

  public override string ToString()
    => $"{Street}, {Ward}, {City}, {Province}, {ZipCode}";

  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return ToString();
  }
}
