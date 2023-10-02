using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Profio.Domain.Entities;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Hubs.Queries;

public sealed record GetNearestHubByLocationQuery(Location Location) : IRequest<HubDto>;

public sealed class GetNearestHubByLocationQueryHandler : IRequestHandler<GetNearestHubByLocationQuery, HubDto>
{
  private readonly IRepository<Hub> _hubRepository;
  private readonly IMapper _mapper;

  public GetNearestHubByLocationQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper)
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

    var nearestHubAndDistance = hubAndDistances.MinBy(had => had.Distance);

    return _mapper.Map<HubDto>(nearestHubAndDistance!.Hub);
  }

  private static double CalculateDistance(Location location1, Location location2)
  {
    const double earthRadius = 6371; // Radius of Earth in kilometers
    var lat1Rad = ToRadians(location1.Latitude);
    var lat2Rad = ToRadians(location2.Latitude);
    var deltaLat = ToRadians(location2.Latitude - location1.Latitude);
    var deltaLon = ToRadians(location2.Longitude - location1.Longitude);

    var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
            Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
            Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

    return earthRadius * c;
  }

  private static double ToRadians(double degrees) => degrees * (Math.PI / 180);

  public record HubAndDistance(Hub Hub, double Distance);
}
