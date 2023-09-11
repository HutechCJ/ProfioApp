using Profio.Domain.Constants;

namespace Profio.Application.Vehicles;

public record VehicleEnumFilter(VehicleType? Type, VehicleStatus? Status);
