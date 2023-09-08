using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Incidents.Commands;

public record DeleteIncidentCommand(object Id) : DeleteCommandBase<IncidentDto>(Id);

public class DeleteIncidentCommandHandler : DeleteCommandHandlerBase<DeleteIncidentCommand, IncidentDto, Incident>
{
  public DeleteIncidentCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteIncidentCommandValidator : DeleteCommandValidatorBase<DeleteIncidentCommand, IncidentDto>
{
}
