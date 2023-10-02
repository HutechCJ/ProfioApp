using MediatR;
using Profio.Domain.Constants;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Orders.Queries;

public sealed record GetOrderCountByStatusQuery : IRequest<IEnumerable<int>>;

public sealed class GetOrderCountByStatusQueryHandler : IRequestHandler<GetOrderCountByStatusQuery, IEnumerable<int>>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetOrderCountByStatusQueryHandler(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;

  public Task<IEnumerable<int>> Handle(GetOrderCountByStatusQuery request, CancellationToken cancellationToken)
    => Task.FromResult(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>()
      .Select(status => _applicationDbContext.Orders.Count(v => v.Status == status)));
}
