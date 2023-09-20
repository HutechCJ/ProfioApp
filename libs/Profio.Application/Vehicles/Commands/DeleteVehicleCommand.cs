using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

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
