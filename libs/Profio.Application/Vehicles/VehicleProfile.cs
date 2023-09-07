using Profio.Application.CQRS;
using Profio.Application.Vehicles.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles;

public class VehicleProfile : EntityProfileBase<Vehicle, VehicleDto, CreateVehicleCommand, UpdateVehicleCommand> { }
