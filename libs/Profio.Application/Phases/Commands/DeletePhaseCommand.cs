using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Phases.Commands;

public sealed record DeletePhaseCommand(object Id) : DeleteCommandBase<PhaseDto>(Id);

public sealed class DeletePhaseCommandHandler : DeleteCommandHandlerBase<DeletePhaseCommand, PhaseDto, Phase>
{
  public DeletePhaseCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeletePhaseCommandValidator : DeleteCommandValidatorBase<DeletePhaseCommand, PhaseDto>
{
}
