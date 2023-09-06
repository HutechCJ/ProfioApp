using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Exceptions;

namespace Profio.Application.Hubs.Queries;

public record GetNearestHubByLocationQuery(Location Location) : IRequest<ResultModel<HubDto>>;
public class GetNearestHubByLocationQueryHandler : IRequestHandler<GetNearestHubByLocationQuery, ResultModel<HubDto>>
{
  private readonly IRepository<Hub> _hubRepository;
  private readonly IMapper _mapper;

  public GetNearestHubByLocationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _hubRepository = unitOfWork.Repository<Hub>();
    _mapper = mapper;
  }
  public async Task<ResultModel<HubDto>> Handle(GetNearestHubByLocationQuery request, CancellationToken cancellationToken)
  {
    var query = _hubRepository
      .SingleResultQuery()
      .AndFilter(hub => hub.Location != null)
      .OrderBy(hub => CalculateDistance(request.Location, hub.Location!));

    var dto = await _hubRepository
      .ToQueryable(query)
      .AsSplitQuery()
      .ProjectTo<HubDto>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Hub).Name);

    return new(dto);
  }
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
