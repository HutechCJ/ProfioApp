using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Hubs.Validators;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.Exceptions;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Vehicles.Commands;

public sealed record VisitHubCommand(string VehicleId, string HubId) : IRequest<Unit>;

public sealed class VisitHubCommandHandler : IRequestHandler<VisitHubCommand, Unit>
{
  private readonly ApplicationDbContext _applicationDbContext;
  private readonly IRepository<Order> _orderRepository;
  private readonly IUnitOfWork _unitOfWork;

  public VisitHubCommandHandler(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
  {
    _applicationDbContext = applicationDbContext;
    _unitOfWork = unitOfWork;
    _orderRepository = unitOfWork.Repository<Order>();
  }

  public async Task<Unit> Handle(VisitHubCommand request, CancellationToken cancellationToken)
  {
    var hub = await _applicationDbContext.Hubs
                .Where(v => v.Id == request.HubId)
                .FirstOrDefaultAsync(cancellationToken) ??
              throw new NotFoundException(nameof(Hub), request.HubId);

    var vehicle = await _applicationDbContext.Vehicles
                    .Include(v => v.Deliveries)
                    .ThenInclude(d => d.Order)
                    .Where(v => v.Id == request.VehicleId)
                    .FirstOrDefaultAsync(cancellationToken) ??
                  throw new NotFoundException(nameof(Vehicle), request.VehicleId);

    var nextDelivery = vehicle.Deliveries.MaxBy(d => d.DeliveryDate) ?? throw new NotFoundException(nameof(Delivery));

    if (nextDelivery.Order == null)
      return Unit.Value;

    if (hub.ZipCode != nextDelivery.Order.DestinationZipCode)
      return Unit.Value;

    await _orderRepository
      .UpdateAsync(
        o => o.Id == nextDelivery.OrderId,
        o => o.SetProperty(s => s.Status, OrderStatus.Completed), cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

    return Unit.Value;
  }
}

public sealed class VisitHubCommandValidator : AbstractValidator<VisitHubCommand>
{
  public VisitHubCommandValidator(VehicleExistenceByIdValidator vehicleValidator,
    HubExistenceByIdValidator hubValidator)
  {
    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);

    RuleFor(x => x.HubId)
      .SetValidator(hubValidator);
  }
}
