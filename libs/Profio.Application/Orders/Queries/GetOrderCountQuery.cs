using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Orders.Queries;

public record GetOrderCountQuery : GetCountQueryBase;
public class GetOrderCountQueryHandler : GetCountQueryHandlerBase<GetOrderCountQuery, Order>
{
  public GetOrderCountQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
  {
  }
}
