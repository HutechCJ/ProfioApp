using Profio.Domain.Constants;

namespace Profio.Application.Vehicles;

public sealed record VehicleEnumFilter(VehicleType? Type, VehicleStatus? Status);
