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
  Title = "Order Create Request",
  Description = "A Representation of list of Order")]
public sealed record CreateOrderCommand : CreateCommandBase
{
  public DateTime StartedDate { get; set; } = DateTime.UtcNow;
  public DateTime? ExpectedDeliveryTime { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Pending;
  public Address? DestinationAddress { get; set; }
  public required string? DestinationZipCode { get; set; }
  public string? Note { get; set; }
  public double? Distance { get; set; }
  public string? CustomerId { get; set; }
  public string? PhaseId { get; set; }
}

public sealed class CreateOrderCommandHandler : CreateCommandHandlerBase<CreateOrderCommand, Order>
{
  public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
  public CreateOrderCommandValidator(CustomerExistenceByIdValidator customerIdValidator, PhaseExistenceByIdValidator phaseValidator)
  {
    RuleFor(c => c.ExpectedDeliveryTime)
      .GreaterThan(c => c.StartedDate);

    RuleFor(c => c.Status)
      .IsInEnum();

    RuleFor(c => c.DestinationZipCode)
      .Matches("^[0-9]*$")
      .MaximumLength(10);

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
