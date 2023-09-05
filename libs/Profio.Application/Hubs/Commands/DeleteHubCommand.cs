using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs.Commands;

public record DeleteHubCommand(object Id) : DeleteCommandBase<HubDto>(Id);

public class DeleteHubCommandHandler : DeleteCommandHandlerBase<DeleteHubCommand, HubDto, Hub>
{
  public DeleteHubCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteHubCommandValidator : DeleteCommandValidatorBase<DeleteHubCommand, HubDto>
{
}
