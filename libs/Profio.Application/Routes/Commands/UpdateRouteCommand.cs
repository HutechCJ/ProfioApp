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
  Title = "Route Update Request",
  Description = "A Representation of list of Route")]
public sealed record UpdateRouteCommand(object Id) : UpdateCommandBase(Id)
{
  public double? Distance { get; set; }
  public string? StartHubId { get; set; }
  public string? EndHubId { get; set; }
}

public sealed class UpdateRouteCommandHandler : UpdateCommandHandlerBase<UpdateRouteCommand, Route>
{
  public UpdateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class UpdateRouteCommandValidator : AbstractValidator<UpdateRouteCommand>
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
