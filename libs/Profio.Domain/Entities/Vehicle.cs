using Profio.Domain.Constants;

namespace Profio.Domain.Entities;

public class Vehicle
{
  public required string? Id { get; set; } = Ulid.NewUlid().ToString();
  public string? ZipCodeCurrent { get; set; }
  public string? LicensePlate { get; set; }
  public VehicleType VehicleType { get; set; } = VehicleType.Truck;
  public string? StaffId { get; set; }
  public Staff? Staff { get; set; }
  public ICollection<Order> Orders { get; set; } = new List<Order>();
}
