using Profio.Application.Staffs;
using Profio.Domain.Constants;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Vehicles;

[SwaggerSchema(
  Title = "Vehicle",
  Description = "A Representation of list of Vehicle")]
public sealed record VehicleDto : BaseModel
{
  public required string Id { get; set; }
  public string? ZipCodeCurrent { get; set; }
  public string? LicensePlate { get; set; }
  public VehicleType Type { get; set; } = VehicleType.Truck;
  public VehicleStatus Status { get; set; } = VehicleStatus.Idle;
  public StaffDto? Staff { get; set; }
}
