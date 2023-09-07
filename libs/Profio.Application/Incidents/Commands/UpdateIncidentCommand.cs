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
  Title = "Incident Update Request",
  Description = "A Representation of list of Incident")]
public record UpdateIncidentCommand(object Id) : UpdateCommandBase(Id)
{
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; }
  public DateTime? Time { get; set; }
  public string? OrderHistoryId { get; set; }
}

public class UpdateIncidentCommandHandler : UpdateCommandHandlerBase<UpdateIncidentCommand, Incident>
{
  public UpdateIncidentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class UpdateIncidentCommandValidator : AbstractValidator<UpdateIncidentCommand>
{
  public UpdateIncidentCommandValidator(OrderHistoryExistenceByIdValidator orderHistoryIdValidator)
  {
    RuleFor(c => c.Description)
      .MaximumLength(250);

    RuleFor(c => c.Status)
      .IsInEnum();

    RuleFor(c => c.OrderHistoryId)
      .SetValidator(orderHistoryIdValidator!);
  }
}
