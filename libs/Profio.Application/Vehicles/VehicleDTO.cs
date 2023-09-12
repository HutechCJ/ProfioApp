using Profio.Application.Staffs;
using Profio.Domain.Constants;
using Profio.Domain.Models;

namespace Profio.Application.Vehicles;

public record VehicleDto : BaseModel
{
  public required string Id { get; set; }
  public string? ZipCodeCurrent { get; set; }
  public string? LicensePlate { get; set; }
  public VehicleType Type { get; set; } = VehicleType.Truck;
  public VehicleStatus Status { get; set; } = VehicleStatus.Idle;
  public StaffDto? Staff { get; set; }
}
