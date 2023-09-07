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
  Title = "Route Update Request",
  Description = "A Representation of list of Route")]
public record UpdateRouteCommand(object Id) : UpdateCommandBase(Id)
{
  public double? Distance { get; set; }
  public string? StartHubId { get; set; }
  public string? EndHubId { get; set; }
}

public class UpdateRouteCommandHandler : UpdateCommandHandlerBase<UpdateRouteCommand, Route>
{
  public UpdateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class UpdateRouteCommandValidator : AbstractValidator<UpdateRouteCommand>
{
  public UpdateRouteCommandValidator(HubExistenceByIdValidator hubValidator)
  {
    RuleFor(r => r.Distance)
      .GreaterThanOrEqualTo(0);

    RuleFor(r => r.StartHubId)
      .SetValidator(hubValidator!);

    RuleFor(r => r.EndHubId)
      .SetValidator(hubValidator!);
  }
}
