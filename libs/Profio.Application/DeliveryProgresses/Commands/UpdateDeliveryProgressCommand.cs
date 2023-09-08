using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Application.Orders.Validators;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.DeliveryProgresses.Commands;

[SwaggerSchema(
  Title = "Delivery Progress Update Request",
  Description = "A Representation of list of Delivery Progress")]
public record UpdateDeliveryProgressCommand(object Id) : UpdateCommandBase(Id)
{
  public Location? CurrentLocation { get; set; }
  public byte PercentComplete { get; set; } = 0;
  //public TimeSpan? EstimatedTimeRemaining { get; set; }
  public DateTime? LastUpdated { get; set; }
  public string? OrderId { get; set; }
}

public class UpdateDeliveryProgressCommandHandler : UpdateCommandHandlerBase<UpdateDeliveryProgressCommand, DeliveryProgress>
{
  public UpdateDeliveryProgressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class UpdateDeliveryProgressCommandValidator : UpdateCommandValidatorBase<UpdateDeliveryProgressCommand>
{
  public UpdateDeliveryProgressCommandValidator(OrderExistenceByIdValidator orderValidator)
  {
    RuleFor(c => c.CurrentLocation)
      .SetValidator(new LocationValidator()!);

    RuleFor(c => c.PercentComplete);

    RuleFor(c => c.OrderId)
      .SetValidator(orderValidator!);
  }
}
