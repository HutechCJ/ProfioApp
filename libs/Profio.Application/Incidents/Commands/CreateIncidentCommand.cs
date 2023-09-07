using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.OrderHistories.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Incidents.Commands;

[SwaggerSchema(
  Title = "Incident Create Request",
  Description = "A Representation of list of Incident")]
public record CreateIncidentCommand : CreateCommandBase
{
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; }
  public DateTime? Time { get; set; }
  public required string OrderHistoryId { get; set; }
}

public class CreateIncidentCommandHandler : CreateCommandHandlerBase<CreateIncidentCommand, Incident>
{
  public CreateIncidentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class CreateIncidentCommandValidator : AbstractValidator<CreateIncidentCommand>
{
  public CreateIncidentCommandValidator(OrderHistoryExistenceByIdValidator orderHistoryIdValidator)
  {
    RuleFor(c => c.Description)
      .MaximumLength(250);

    RuleFor(c => c.Status)
      .IsInEnum();

    RuleFor(c => c.OrderHistoryId)
      .SetValidator(orderHistoryIdValidator);
  }
}
