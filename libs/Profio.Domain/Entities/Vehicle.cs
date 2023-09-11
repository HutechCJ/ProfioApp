using Profio.Domain.Constants;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public sealed class Vehicle : IEntity
{
  public string Id { get; set; } = Ulid.NewUlid().ToString()!;
  public string? ZipCodeCurrent { get; set; }
  public string? LicensePlate { get; set; }
  public VehicleType Type { get; set; } = VehicleType.Truck;
  public VehicleStatus Status { get; set; } = VehicleStatus.Idle;
  public string? StaffId { get; set; }
  public Staff? Staff { get; set; }
  public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
