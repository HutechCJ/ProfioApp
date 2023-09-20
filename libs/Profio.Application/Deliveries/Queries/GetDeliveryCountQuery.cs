using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Deliveries.Queries;

public sealed record GetDeliveryCountQuery : GetCountQueryBase;
public sealed class GetDeliveryCountQueryHandler : GetCountQueryHandlerBase<GetDeliveryCountQuery, Delivery>
{
  public GetDeliveryCountQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
  {
  }
}
