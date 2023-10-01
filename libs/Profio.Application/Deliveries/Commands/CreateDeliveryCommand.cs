using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Orders.Validators;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Deliveries.Commands;

[SwaggerSchema(
  Title = "Create Delivery",
  Description = "A Representation of list of Delivery")]
public sealed record CreateDeliveryCommand : CreateCommandBase
{
  public DateTime? DeliveryDate { get; set; } = DateTime.UtcNow;
  public required string OrderId { get; set; }
  public required string VehicleId { get; set; }
}

public sealed class CreateDeliveryCommandHandler : CreateCommandHandlerBase<CreateDeliveryCommand, Delivery>
{
  public CreateDeliveryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class CreateDeliveryCommandValidator : AbstractValidator<CreateDeliveryCommand>
{
  public CreateDeliveryCommandValidator(OrderExistenceByIdValidator orderValidator, VehicleExistenceByIdValidator vehicleValidator)
  {
    RuleFor(x => x.OrderId)
      .SetValidator(orderValidator);

    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);
  }
}
