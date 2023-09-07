using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Application.Orders.Validators;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.DeliveryProgresses.Commands;

[SwaggerSchema(
  Title = "Delivery Progress Create Request",
  Description = "A Representation of list of Delivery Progress")]
public record CreateDeliveryProgressCommand : CreateCommandBase
{
  public Location? CurrentLocation { get; set; }
  public byte PercentComplete { get; set; } = 0;
  //public TimeSpan? EstimatedTimeRemaining { get; set; }
  public DateTime? LastUpdated { get; set; }
  public required string OrderId { get; set; }

}

public class CreateDeliveryProgressCommandHandler : CreateCommandHandlerBase<CreateDeliveryProgressCommand, DeliveryProgress>
{
  public CreateDeliveryProgressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class CreateDeliveryProgressCommandValidator : AbstractValidator<CreateDeliveryProgressCommand>
{
  public CreateDeliveryProgressCommandValidator(OrderExistenceByIdValidator orderValidator)
  {
    RuleFor(c => c.CurrentLocation)
      .SetValidator(new LocationValidator()!);

    RuleFor(c => c.PercentComplete);

    RuleFor(c => c.OrderId)
      .SetValidator(orderValidator);
  }
}
