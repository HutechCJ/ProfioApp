using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Routes.Validators;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;

namespace Profio.Application.Phases.Commands;

public sealed record CreatePhaseCommand : CreateCommandBase
{
  public int Order { get; set; } = 1;
  public string? RouteId { get; set; }
};
sealed class CreatePhaseCommandHandler : CreateCommandHandlerBase<CreatePhaseCommand, Phase>
{
  public CreatePhaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
public sealed class CreatePhaseCommandValidator : AbstractValidator<CreatePhaseCommand>
{
  public CreatePhaseCommandValidator(RouteExistenceByIdValidator routeValidator)
  {
    RuleFor(x => x.Order)
      .GreaterThanOrEqualTo(1);

    RuleFor(x => x.RouteId)
      .SetValidator(routeValidator!);
  }
}
