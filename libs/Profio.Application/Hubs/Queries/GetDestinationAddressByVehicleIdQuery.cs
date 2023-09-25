using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Entities;
using Profio.Domain.Exceptions;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Hubs.Queries;

public sealed record GetDestinationAddressByVehicleIdQuery(string VehicleId) : IRequest<Address>;
public sealed class GetDestinationAddressByVehicleIdQueryHandler : IRequestHandler<GetDestinationAddressByVehicleIdQuery, Address>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetDestinationAddressByVehicleIdQueryHandler(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;
  public async Task<Address> Handle(GetDestinationAddressByVehicleIdQuery request, CancellationToken cancellationToken)
  {
    var vehicle = await _applicationDbContext.Vehicles
        .Include(v => v.Deliveries)
        .ThenInclude(d => d.Order)
        .ThenInclude(o => o!.Customer)
        .Where(v => v.Id == request.VehicleId)
        .FirstOrDefaultAsync(cancellationToken) ??
      throw new NotFoundException(nameof(Vehicle), request.VehicleId);

    var nextDelivery = vehicle.Deliveries.MaxBy(d => d.DeliveryDate)
        ?? throw new NotFoundException(nameof(Delivery));

    var order = nextDelivery.Order
      ?? throw new NotFoundException(nameof(Order));

    var customer = order.Customer
      ?? throw new NotFoundException(nameof(Customer));

    var address = customer.Address
      ?? throw new NotFoundException(nameof(Address));

    return address;
  }
}
