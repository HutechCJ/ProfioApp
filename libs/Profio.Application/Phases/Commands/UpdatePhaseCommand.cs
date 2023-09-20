using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Application.Routes.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Phases.Commands;

public sealed record UpdatePhaseCommand(object Id) : UpdateCommandBase(Id)
{
  public int? Order { get; set; }
  public string? RouteId { get; set; }
}
sealed class UpdatePhaseCommandHandler : UpdateCommandHandlerBase<UpdatePhaseCommand, Phase>
{
  public UpdatePhaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
public sealed class UpdatePhaseCommandValidator : UpdateCommandValidatorBase<UpdatePhaseCommand>
{
  public UpdatePhaseCommandValidator(RouteExistenceByIdValidator routeValidator)
  {
    RuleFor(x => x.Order)
    .GreaterThanOrEqualTo(1);

    RuleFor(x => x.RouteId)
      .SetValidator(routeValidator!);
  }
}
