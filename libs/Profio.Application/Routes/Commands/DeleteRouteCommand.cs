using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Routes.Commands;

public record DeleteRouteCommand(object Id) : DeleteCommandBase<RouteDto>(Id);

public class DeleteRouteCommandHandler : DeleteCommandHandlerBase<DeleteRouteCommand, RouteDto, Route>
{
  public DeleteRouteCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteRouteCommandValidator : DeleteCommandValidatorBase<DeleteRouteCommand, RouteDto>
{
}
