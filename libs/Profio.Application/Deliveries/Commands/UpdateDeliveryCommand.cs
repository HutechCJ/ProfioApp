using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Application.CQRS.Validators;
using Profio.Application.Orders.Validators;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Deliveries.Commands;

public record UpdateDeliveryCommand(object Id) : UpdateCommandBase(Id)
{
  public DateTime? DeliveryDate { get; set; }
  public string? OrderId { get; set; }
  public string? VehicleId { get; set; }
};
public class UpdateDeliveryCommandHandler : UpdateCommandHandlerBase<UpdateDeliveryCommand, Delivery>
{
  public UpdateDeliveryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
public class UpdateDeliveryCommandValidator : UpdateCommandValidatorBase<UpdateDeliveryCommand>
{
  public UpdateDeliveryCommandValidator(OrderExistenceByIdValidator orderValidator, VehicleExistenceByIdValidator vehicleValidator)
  {
    RuleFor(x => x.OrderId)
      .SetValidator(orderValidator!);

    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator!);
  }
}
