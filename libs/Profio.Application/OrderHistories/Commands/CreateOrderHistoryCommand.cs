using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.Deliveries.Validators;
using Profio.Application.Hubs.Validators;
using Profio.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.OrderHistories.Commands;

[SwaggerSchema(
  Title = "Order History Request",
  Description = "A Representation of list of Order History")]
public record CreateOrderHistoryCommand : CreateCommandBase
{
  public DateTime? Timestamp { get; set; }
  public required string DeliveryId { get; set; }
  public required string HubId { get; set; }
}

public class CreateOrderHistoryCommandHandler : CreateCommandHandlerBase<CreateOrderHistoryCommand, OrderHistory>
{
  public CreateOrderHistoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class CreateOrderHistoryCommandValidator : AbstractValidator<CreateOrderHistoryCommand>
{
  public CreateOrderHistoryCommandValidator(DeliveryExistenceByIdValidator deliveryValidator, HubExistenceByIdValidator hubValidator)
  {
    RuleFor(x => x.DeliveryId)
      .SetValidator(deliveryValidator);

    RuleFor(x => x.HubId)
      .SetValidator(hubValidator);
  }
}
