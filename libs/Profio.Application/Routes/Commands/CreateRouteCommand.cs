using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.Hubs.Validators;
using Profio.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Routes.Commands;

[SwaggerSchema(
  Title = "Route Create Request",
  Description = "A Representation of list of Route")]
public record CreateRouteCommand : CreateCommandBase
{
  public double? Distance { get; set; }
  public required string StartHubId { get; set; }
  public required string EndHubId { get; set; }
}

public class CreateRouteCommandHandler : CreateCommandHandlerBase<CreateRouteCommand, Route>
{
  public CreateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class CreateRouteCommandValidator : AbstractValidator<CreateRouteCommand>
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
