using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Hubs.Validators;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Infrastructure.Exceptions;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Vehicles.Commands;

public record VisitHubCommand(string VehicleId, string HubId) : IRequest<Unit>;
public class VisitHubCommandHandler : IRequestHandler<VisitHubCommand, Unit>
{
  private readonly ApplicationDbContext _applicationDbContext;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRepository<OrderHistory> _orderHistoryRepository;
  private readonly IRepository<Order> _orderRepository;

  public VisitHubCommandHandler(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
  {
    _applicationDbContext = applicationDbContext;
    _unitOfWork = unitOfWork;
    _orderHistoryRepository = unitOfWork.Repository<OrderHistory>();
    _orderRepository = unitOfWork.Repository<Order>();
  }

  public async Task<Unit> Handle(VisitHubCommand request, CancellationToken cancellationToken)
  {
    var hub = await _applicationDbContext.Hubs
        .Where(v => v.Id == request.HubId)
        .FirstOrDefaultAsync(cancellationToken) ??
      throw new NotFoundException(typeof(Hub).Name, request.HubId);

    var vehicle = await _applicationDbContext.Vehicles
        .Include(v => v.Deliveries)
        .ThenInclude(d => d.Order)
        .Where(v => v.Id == request.VehicleId)
        .FirstOrDefaultAsync(cancellationToken) ??
      throw new NotFoundException(typeof(Vehicle).Name, request.VehicleId);

    var nextDelivery = vehicle.Deliveries
        .OrderByDescending(d => d.DeliveryDate)
        .FirstOrDefault() ?? throw new NotFoundException(typeof(Delivery).Name);

    if (nextDelivery.Order == null)
      return Unit.Value;

    if (hub.ZipCode != nextDelivery.Order.DestinationZipCode)
      return Unit.Value;

    await _orderRepository
      .UpdateAsync(
        o => o.Id == nextDelivery.OrderId,
        o => o.SetProperty(o => o.Status, OrderStatus.Completed), cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

    return Unit.Value;
  }
}
public class VisitHubCommandValidator : AbstractValidator<VisitHubCommand>
{
  public VisitHubCommandValidator(VehicleExistenceByIdValidator vehicleValidator, HubExistenceByIdValidator hubValidator)
  {
    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);

    RuleFor(x => x.HubId)
      .SetValidator(hubValidator);
  }
}
