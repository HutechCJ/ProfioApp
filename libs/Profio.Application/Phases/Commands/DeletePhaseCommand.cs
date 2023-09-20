using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Phases.Commands;

public sealed record DeletePhaseCommand(object Id) : DeleteCommandBase<PhaseDto>(Id);

sealed class DeletePhaseCommandHandler : DeleteCommandHandlerBase<DeletePhaseCommand, PhaseDto, Phase>
{
  public DeletePhaseCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeletePhaseCommandValidator : DeleteCommandValidatorBase<DeletePhaseCommand, PhaseDto>
{
}
