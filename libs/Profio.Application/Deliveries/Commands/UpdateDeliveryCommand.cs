using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Orders.Validators;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Deliveries.Commands;

public sealed record UpdateDeliveryCommand(object Id) : UpdateCommandBase(Id)
{
  public DateTime? DeliveryDate { get; set; }
  public string? OrderId { get; set; }
  public string? VehicleId { get; set; }
};
public sealed class UpdateDeliveryCommandHandler : UpdateCommandHandlerBase<UpdateDeliveryCommand, Delivery>
{
  public UpdateDeliveryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
public sealed class UpdateDeliveryCommandValidator : UpdateCommandValidatorBase<UpdateDeliveryCommand>
{
  public UpdateDeliveryCommandValidator(OrderExistenceByIdValidator orderValidator, VehicleExistenceByIdValidator vehicleValidator)
  {
    RuleFor(x => x.OrderId)
      .SetValidator(orderValidator!);

    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator!);
  }
}
