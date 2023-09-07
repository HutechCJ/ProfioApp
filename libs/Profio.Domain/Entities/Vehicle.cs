using Profio.Domain.Constants;

namespace Profio.Domain.Entities;

public class Vehicle : BaseEntity
{
  public string? ZipCodeCurrent { get; set; }
  public string? LicensePlate { get; set; }
  public VehicleType Type { get; set; } = VehicleType.Truck;
  public VehicleStatus Status { get; set; } = VehicleStatus.Idle;
  public string? StaffId { get; set; }
  public Staff? Staff { get; set; }
  public ICollection<Delivery>? Deliveries { get; set; } = new List<Delivery>();
}
