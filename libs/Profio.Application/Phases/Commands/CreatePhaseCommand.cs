using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Routes.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Phases.Commands;

public sealed record CreatePhaseCommand : CreateCommandBase
{
  public int Order { get; set; } = 0;
  public string? RouteId { get; set; }
};
sealed class CreatePhaseCommandHandler : CreateCommandHandlerBase<CreatePhaseCommand, Phase>
{
  public CreatePhaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
sealed class CreatePhaseCommandValidator : AbstractValidator<CreatePhaseCommand>
{
  public CreatePhaseCommandValidator(RouteExistenceByIdValidator routeValidator)
  {
    RuleFor(x => x.Order)
      .GreaterThanOrEqualTo(0);

    RuleFor(x => x.RouteId)
      .SetValidator(routeValidator!);
  }
}
