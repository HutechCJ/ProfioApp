using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Vehicles.Commands;

[SwaggerSchema(
  Title = "Update Vehicle",
  Description = "A Representation of list of Vehicle")]
public sealed record UpdateVehicleCommand(object Id) : UpdateCommandBase(Id)
{
  public string? ZipCodeCurrent { get; set; }
  public string? LicensePlate { get; set; }
  public VehicleType? Type { get; set; } = VehicleType.Truck;
  public VehicleStatus? Status { get; set; } = VehicleStatus.Idle;
  public string? StaffId { get; set; }
}

public sealed class UpdateVehicleCommandHandler : UpdateCommandHandlerBase<UpdateVehicleCommand, Vehicle>
{
  public UpdateVehicleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class UpdateVehicleCommandValidator : UpdateCommandValidatorBase<UpdateVehicleCommand>
{
  public UpdateVehicleCommandValidator(StaffExistenceByIdValidator staffIdValidator)
  {
    RuleFor(x => x.ZipCodeCurrent)
      .MaximumLength(50);

    RuleFor(x => x.LicensePlate)
      .MaximumLength(50);

    RuleFor(x => x.Type)
      .IsInEnum();

    RuleFor(x => x.Status)
      .IsInEnum();

    RuleFor(x => x.StaffId)
      .SetValidator(staffIdValidator!);
  }
}
