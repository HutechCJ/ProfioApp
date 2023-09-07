using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Hubs.Queries;

public record GetNearestHubByLocationQuery(Location Location) : IRequest<HubDto>;
public class GetNearestHubByLocationQueryHandler : IRequestHandler<GetNearestHubByLocationQuery, HubDto>
{
  private readonly IRepository<Hub> _hubRepository;
  private readonly IMapper _mapper;

  public GetNearestHubByLocationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _hubRepository = unitOfWork.Repository<Hub>();
    _mapper = mapper;
  }
  public async Task<HubDto> Handle(GetNearestHubByLocationQuery request, CancellationToken cancellationToken)
  {
    var getAllQuery = _hubRepository
      .MultipleResultQuery();

    var hubs = await _hubRepository
      .SearchAsync(getAllQuery, cancellationToken);

    var hubAndDistances = hubs.Select(h => new HubAndDistance(h, CalculateDistance(h.Location!, request.Location)));

    var nearestHubAndDistance = hubAndDistances.OrderBy(had => had.Distance).FirstOrDefault();

    return _mapper.Map<HubDto>(nearestHubAndDistance!.Hub);
  }
  record HubAndDistance(Hub Hub, double Distance);
  private static double CalculateDistance(Location location1, Location location2)
  {
    // Haversine formula for calculating distance between two points on Earth's surface
    double earthRadius = 6371; // Radius of Earth in kilometers
    double lat1Rad = ToRadians(location1.Latitude);
    double lat2Rad = ToRadians(location2.Latitude);
    double deltaLat = ToRadians(location2.Latitude - location1.Latitude);
    double deltaLon = ToRadians(location2.Longitude - location1.Longitude);

    double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
               Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
               Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

    return earthRadius * c;
  }

  private static double ToRadians(double degrees)
  {
    return degrees * (Math.PI / 180);
  }
}
