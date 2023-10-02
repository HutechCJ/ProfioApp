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
  Title = "Update Incident",
  Description = "A Representation of list of Incident")]
public sealed record UpdateIncidentCommand(object Id) : UpdateCommandBase(Id)
{
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; }
  public DateTime? Time { get; set; }
  public string? OrderHistoryId { get; set; }
}

public sealed class UpdateIncidentCommandHandler : UpdateCommandHandlerBase<UpdateIncidentCommand, Incident>
{
  public UpdateIncidentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class UpdateIncidentCommandValidator : AbstractValidator<UpdateIncidentCommand>
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
