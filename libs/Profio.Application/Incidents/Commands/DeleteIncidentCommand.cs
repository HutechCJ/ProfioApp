using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Incidents.Commands;

public sealed record DeleteIncidentCommand(object Id) : DeleteCommandBase<IncidentDto>(Id);

public sealed class DeleteIncidentCommandHandler : DeleteCommandHandlerBase<DeleteIncidentCommand, IncidentDto, Incident>
{
  public DeleteIncidentCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteIncidentCommandValidator : DeleteCommandValidatorBase<DeleteIncidentCommand, IncidentDto>
{
}
