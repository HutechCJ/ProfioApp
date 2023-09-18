using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Commands;

public sealed record DeleteVehicleCommand(object Id) : DeleteCommandBase<VehicleDto>(Id);

public sealed class DeleteVehicleCommandHandler : DeleteCommandHandlerBase<DeleteVehicleCommand, VehicleDto, Vehicle>
{
  public DeleteVehicleCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteVehicleCommandValidator : DeleteCommandValidatorBase<DeleteVehicleCommand, VehicleDto>
{
}
