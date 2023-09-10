using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Exceptions;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Hubs.Queries;

public record GetDestinationAddressByVehicleIdQuery(string VehicleId) : IRequest<Address>;
public class GetDestinationAddressByVehicleIdQueryHandler : IRequestHandler<GetDestinationAddressByVehicleIdQuery, Address>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetDestinationAddressByVehicleIdQueryHandler(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;
  public async Task<Address> Handle(GetDestinationAddressByVehicleIdQuery request, CancellationToken cancellationToken)
  {
    var vehicle = await _applicationDbContext.Vehicles
        .Include(v => v.Deliveries)
        .ThenInclude(d => d.Order)
        .ThenInclude(o => o.Customer)
        .Where(v => v.Id == request.VehicleId)
        .FirstOrDefaultAsync(cancellationToken) ??
      throw new NotFoundException(typeof(Vehicle).Name, request.VehicleId);

    var nextDelivery = vehicle.Deliveries
        .OrderByDescending(d => d.DeliveryDate)
        .FirstOrDefault() ?? throw new NotFoundException(typeof(Delivery).Name);

    var order = nextDelivery.Order ?? throw new NotFoundException(typeof(Order).Name);

    var customer = order.Customer ?? throw new NotFoundException(typeof(Customer).Name);

    var address = customer.Address ?? throw new NotFoundException(typeof(Address).Name);

    return address;
  }
}
