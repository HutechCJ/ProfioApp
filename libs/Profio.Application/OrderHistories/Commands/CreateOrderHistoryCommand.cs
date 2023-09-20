using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Deliveries.Validators;
using Profio.Application.Hubs.Validators;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.OrderHistories.Commands;

[SwaggerSchema(
  Title = "Order History Request",
  Description = "A Representation of list of Order History")]
public sealed record CreateOrderHistoryCommand : CreateCommandBase
{
  public DateTime? Timestamp { get; set; }
  public required string DeliveryId { get; set; }
  public required string HubId { get; set; }
}

public sealed class CreateOrderHistoryCommandHandler : CreateCommandHandlerBase<CreateOrderHistoryCommand, OrderHistory>
{
  public CreateOrderHistoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class CreateOrderHistoryCommandValidator : AbstractValidator<CreateOrderHistoryCommand>
{
  public CreateOrderHistoryCommandValidator(DeliveryExistenceByIdValidator deliveryValidator, HubExistenceByIdValidator hubValidator)
  {
    RuleFor(x => x.DeliveryId)
      .SetValidator(deliveryValidator);

    RuleFor(x => x.HubId)
      .SetValidator(hubValidator);
  }
}
