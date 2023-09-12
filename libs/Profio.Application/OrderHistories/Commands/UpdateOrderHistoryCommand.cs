using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Deliveries.Validators;
using Profio.Application.Hubs.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.OrderHistories.Commands;
public record UpdateOrderHistoryCommand(object Id) : UpdateCommandBase(Id)
{
  public DateTime? Timestamp { get; set; }
  public string? DeliveryId { get; set; }
  public string? HubId { get; set; }
}

public class UpdateOrderHistoryCommandHandler : UpdateCommandHandlerBase<UpdateOrderHistoryCommand, OrderHistory>
{
  public UpdateOrderHistoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class UpdateOrderHistoryCommandValidator : AbstractValidator<UpdateOrderHistoryCommand>
{
  public UpdateOrderHistoryCommandValidator(DeliveryExistenceByIdValidator deliveryValidator, HubExistenceByIdValidator hubValidator)
  {
    RuleFor(x => x.DeliveryId)
      .SetValidator(deliveryValidator!);

    RuleFor(x => x.HubId)
      .SetValidator(hubValidator!);
  }
}
