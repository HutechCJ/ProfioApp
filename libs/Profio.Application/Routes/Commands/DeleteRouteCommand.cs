using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Routes.Commands;

public sealed record DeleteRouteCommand(object Id) : DeleteCommandBase<RouteDto>(Id);

public sealed class DeleteRouteCommandHandler : DeleteCommandHandlerBase<DeleteRouteCommand, RouteDto, Route>
{
  public DeleteRouteCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteRouteCommandValidator : DeleteCommandValidatorBase<DeleteRouteCommand, RouteDto>
{
}
