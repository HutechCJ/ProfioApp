using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Vehicles.Commands;

public record UpdateVehicleCommand(object Id) : UpdateCommandBase(Id)
{
    public string? ZipCodeCurrent { get; set; }
    public string? LicensePlate { get; set; }
    public VehicleType Type { get; set; } = VehicleType.Truck;
    public VehicleStatus Status { get; set; } = VehicleStatus.Idle;
    public string? StaffId { get; set; }
};
public class UpdateVehicleCommandHandler : UpdateCommandHandlerBase<UpdateVehicleCommand, Vehicle>
{
    public UpdateVehicleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
public class UpdateVehicleCommandValidator : UpdateCommandValidatorBase<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
    }
}
