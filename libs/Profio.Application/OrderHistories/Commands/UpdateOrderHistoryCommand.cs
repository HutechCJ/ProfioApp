using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Deliveries.Validators;
using Profio.Application.Hubs.Validators;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;

namespace Profio.Application.OrderHistories.Commands;
public sealed record UpdateOrderHistoryCommand(object Id) : UpdateCommandBase(Id)
{
  public DateTime? Timestamp { get; set; }
  public string? DeliveryId { get; set; }
  public string? HubId { get; set; }
}

public sealed class UpdateOrderHistoryCommandHandler : UpdateCommandHandlerBase<UpdateOrderHistoryCommand, OrderHistory>
{
  public UpdateOrderHistoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class UpdateOrderHistoryCommandValidator : AbstractValidator<UpdateOrderHistoryCommand>
{
  public UpdateOrderHistoryCommandValidator(DeliveryExistenceByIdValidator deliveryValidator, HubExistenceByIdValidator hubValidator)
  {
    RuleFor(x => x.DeliveryId)
      .SetValidator(deliveryValidator!);

    RuleFor(x => x.HubId)
      .SetValidator(hubValidator!);
  }
}
