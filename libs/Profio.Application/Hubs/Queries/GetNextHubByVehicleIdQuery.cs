using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Entities;
using Profio.Infrastructure.Exceptions;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Hubs.Queries;

public sealed record GetNextHubByVehicleIdQuery(string VehicleId) : IRequest<HubDto>;
public sealed class GeNextHubByVehicleIdQueryHandler : IRequestHandler<GetNextHubByVehicleIdQuery, HubDto>
{
  private readonly ApplicationDbContext _applicationDbContext;
  private readonly IMapper _mapper;

  public GeNextHubByVehicleIdQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
  {
    _applicationDbContext = applicationDbContext;
    _mapper = mapper;
  }
  public async Task<HubDto> Handle(GetNextHubByVehicleIdQuery request, CancellationToken cancellationToken)
  {
    var vehicle = await _applicationDbContext.Vehicles
        .Include(v => v.Deliveries)
        .ThenInclude(d => d.Order)
        .Where(v => v.Id == request.VehicleId)
        .FirstOrDefaultAsync(cancellationToken) ??
      throw new NotFoundException(typeof(Vehicle).Name, request.VehicleId);

    var nextDelivery = vehicle.Deliveries
        .OrderByDescending(d => d.DeliveryDate)
        .FirstOrDefault() ?? throw new NotFoundException(typeof(Delivery).Name);

    var destinationZipCode = nextDelivery.Order?.DestinationZipCode ?? throw new NotFoundException(typeof(Order).Name);

    var hubDto = await _applicationDbContext.Hubs
      .ProjectTo<HubDto>(_mapper.ConfigurationProvider)
      .FirstOrDefaultAsync(x => x.ZipCode == destinationZipCode, cancellationToken) ?? throw new NotFoundException(typeof(Hub).Name, destinationZipCode);

    return hubDto;
  }
}
public sealed class GetNextHubByVehicleIdQueryValidator : AbstractValidator<GetNextHubByVehicleIdQuery>
{
  public GetNextHubByVehicleIdQueryValidator(VehicleExistenceByIdValidator vehicleValidator)
  {
    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);
  }
}
