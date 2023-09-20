using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Hubs.Commands;

public sealed record DeleteHubCommand(object Id) : DeleteCommandBase<HubDto>(Id);

public sealed class DeleteHubCommandHandler : DeleteCommandHandlerBase<DeleteHubCommand, HubDto, Hub>
{
  public DeleteHubCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteHubCommandValidator : DeleteCommandValidatorBase<DeleteHubCommand, HubDto>
{
}
