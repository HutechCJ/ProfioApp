using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Vehicles.Commands;

[SwaggerSchema(
  Title = "Vehicle Request",
  Description = "A Representation of list of Vehicle")]
public record CreateVehicleCommand : CreateCommandBase
{
  public string? ZipCodeCurrent { get; set; }
  public string? LicensePlate { get; set; }
  public VehicleType Type { get; set; } = VehicleType.Truck;
  public VehicleStatus Status { get; set; } = VehicleStatus.Idle;
  public string? StaffId { get; set; }
}

public class CreateVehicleCommandHandler : CreateCommandHandlerBase<CreateVehicleCommand, Vehicle>
{
  public CreateVehicleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
  public CreateVehicleCommandValidator(StaffExistenceByIdValidator staffIdValidator)
  {
    RuleFor(x => x.ZipCodeCurrent)
      .MaximumLength(10)
      .Matches("^[0-9]*$");

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
