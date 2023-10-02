using MediatR;
using Profio.Domain.Constants;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetVehicleCountByStatusQuery : IRequest<IEnumerable<int>>;

public sealed class
  GetVehicleCountByStatusQueryHandler : IRequestHandler<GetVehicleCountByStatusQuery, IEnumerable<int>>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetVehicleCountByStatusQueryHandler(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;

  public Task<IEnumerable<int>> Handle(GetVehicleCountByStatusQuery request, CancellationToken cancellationToken)
    => Task.FromResult(Enum.GetValues(typeof(VehicleStatus)).Cast<VehicleStatus>()
      .Select(status => _applicationDbContext.Vehicles.Count(v => v.Status == status)));
}
