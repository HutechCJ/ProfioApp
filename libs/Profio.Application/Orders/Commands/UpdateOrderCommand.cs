using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Customers.Validators;
using Profio.Application.Phases.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Orders.Commands;

[SwaggerSchema(
  Title = "Order Update Request",
  Description = "A Representation of list of Order")]
public sealed record UpdateOrderCommand(object Id) : UpdateCommandBase(Id)
{
  public DateTime? StartedDate { get; set; }
  public DateTime? ExpectedDeliveryTime { get; set; }
  public OrderStatus? Status { get; set; }
  public Address? DestinationAddress { get; set; }
  public required string? DestinationZipCode { get; set; }
  public string? Note { get; set; }
  public double? Distance { get; set; }
  public string? CustomerId { get; set; }
  public string? PhaseId { get; set; }
}

public sealed class UpdateOrderCommandHandler : UpdateCommandHandlerBase<UpdateOrderCommand, Order>
{
  public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
  public UpdateOrderCommandValidator(CustomerExistenceByIdValidator customerIdValidator, PhaseExistenceByIdValidator phaseValidator)
  {
    RuleFor(c => c.ExpectedDeliveryTime)
      .GreaterThan(c => c.StartedDate);

    RuleFor(c => c.Status)
      .IsInEnum();

    RuleFor(c => c.DestinationZipCode)
      .Matches("^[0-9]*$")
      .MaximumLength(50);

    RuleFor(c => c.Note)
      .MaximumLength(250);

    RuleFor(c => c.Distance)
      .GreaterThan(0);

    RuleFor(c => c.DestinationAddress)
      .SetValidator(new AddressValidator()!);

    RuleFor(c => c.CustomerId)
      .SetValidator(customerIdValidator!);

    RuleFor(c => c.PhaseId)
      .SetValidator(phaseValidator!);
  }
}
