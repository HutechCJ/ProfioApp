using MediatR;
using Profio.Domain.Constants;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Staffs.Queries;

public sealed record GetStaffCountByPositionQuery : IRequest<IEnumerable<int>>;

public sealed class
  GetStaffCountByPositionQueryHandler : IRequestHandler<GetStaffCountByPositionQuery, IEnumerable<int>>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetStaffCountByPositionQueryHandler(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;

  public Task<IEnumerable<int>> Handle(GetStaffCountByPositionQuery request, CancellationToken cancellationToken)
  {
    var counts = Enum.GetValues(typeof(Position)).Cast<Position>()
      .Select(position =>
      {
        var count = _applicationDbContext.Staffs.Count(s => s.Position == position);
        return count;
      });
    return Task.FromResult(counts);
  }
}
