using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Deliveries.Queries;

public record GetDeliveryCountQuery : GetCountQueryBase;
public class GetDeliveryCountQueryHandler : GetCountQueryHandlerBase<GetDeliveryCountQuery, Delivery>
{
  public GetDeliveryCountQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
  {
  }
}
