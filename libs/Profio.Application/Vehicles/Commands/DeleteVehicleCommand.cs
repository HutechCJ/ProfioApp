using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Commands;

public record DeleteVehicleCommand(object Id) : DeleteCommandBase<VehicleDto>(Id);

public class DeleteVehicleCommandHandler : DeleteCommandHandlerBase<DeleteVehicleCommand, VehicleDto, Vehicle>
{
  public DeleteVehicleCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteVehicleCommandValidator : DeleteCommandValidatorBase<DeleteVehicleCommand, VehicleDto>
{
}
