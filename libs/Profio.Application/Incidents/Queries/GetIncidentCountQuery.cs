using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Incidents.Queries;

public record GetIncidentCountQuery : GetCountQueryBase;
public class GetIncidentCountQueryHandler : GetCountQueryHandlerBase<GetIncidentCountQuery, Incident>
{
  public GetIncidentCountQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
  {
  }
}
