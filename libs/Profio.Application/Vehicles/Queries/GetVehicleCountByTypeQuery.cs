using MediatR;
using Profio.Domain.Constants;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetVehicleCountByTypeQuery : IRequest<IEnumerable<int>>;

public sealed class GetVehicleCountByTypeQueryHandler : IRequestHandler<GetVehicleCountByTypeQuery, IEnumerable<int>>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetVehicleCountByTypeQueryHandler(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;

  public Task<IEnumerable<int>> Handle(GetVehicleCountByTypeQuery request, CancellationToken cancellationToken)
    => Task.FromResult(Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>()
      .Select(position => _applicationDbContext.Vehicles.Count(v => v.Type == position)));
}
