using Profio.Application.Vehicles.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Vehicles;

public sealed class VehicleProfile : EntityProfileBase<Vehicle, VehicleDto, CreateVehicleCommand, UpdateVehicleCommand> { }
