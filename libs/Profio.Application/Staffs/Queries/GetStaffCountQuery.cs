using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Staffs.Queries;

public sealed record GetStaffCountQuery : GetCountQueryBase;
public sealed class GetStaffCountQueryHandler : GetCountQueryHandlerBase<GetStaffCountQuery, Staff>
{
  public GetStaffCountQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
  {
  }
}
