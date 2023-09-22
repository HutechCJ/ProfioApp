using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Incidents.Queries;

public sealed record GetIncidentCountQuery : GetCountQueryBase;
public sealed class GetIncidentCountQueryHandler : GetCountQueryHandlerBase<GetIncidentCountQuery, Incident>
{
  public GetIncidentCountQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
  {
  }
}
