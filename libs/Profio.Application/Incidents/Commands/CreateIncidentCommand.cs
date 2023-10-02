using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.OrderHistories.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Incidents.Commands;

[SwaggerSchema(
  Title = "Create Incident",
  Description = "A Representation of list of Incident")]
public sealed record CreateIncidentCommand : CreateCommandBase
{
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; }
  public DateTime? Time { get; set; }
}

public sealed class CreateIncidentCommandHandler : CreateCommandHandlerBase<CreateIncidentCommand, Incident>
{
  public CreateIncidentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class CreateIncidentCommandValidator : AbstractValidator<CreateIncidentCommand>
{
  public CreateIncidentCommandValidator(OrderHistoryExistenceByIdValidator orderHistoryIdValidator)
  {
    RuleFor(c => c.Description)
      .MaximumLength(250);

    RuleFor(c => c.Status)
      .IsInEnum();
  }
}
