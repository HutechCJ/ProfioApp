using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Hubs.Validators;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Routes.Commands;

[SwaggerSchema(
  Title = "Route Create Request",
  Description = "A Representation of list of Route")]
public sealed record CreateRouteCommand : CreateCommandBase
{
  public double? Distance { get; set; }
  public required string StartHubId { get; set; }
  public required string EndHubId { get; set; }
}

public sealed class CreateRouteCommandHandler : CreateCommandHandlerBase<CreateRouteCommand, Route>
{
  public CreateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class CreateRouteCommandValidator : AbstractValidator<CreateRouteCommand>
{
  public CreateRouteCommandValidator(HubExistenceByIdValidator hubValidator)
  {
    RuleFor(r => r.Distance)
      .GreaterThanOrEqualTo(0);

    RuleFor(r => r.StartHubId)
      .SetValidator(hubValidator);

    RuleFor(r => r.EndHubId)
      .SetValidator(hubValidator);
  }
}
