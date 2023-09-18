using Profio.Application.Abstractions.CQRS;
using Profio.Application.Vehicles.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles;

public sealed class VehicleProfile : EntityProfileBase<Vehicle, VehicleDto, CreateVehicleCommand, UpdateVehicleCommand> { }
