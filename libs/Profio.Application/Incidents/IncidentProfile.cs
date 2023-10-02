using Profio.Application.Incidents.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Incidents;

public sealed class
  IncidentProfile : EntityProfileBase<Incident, IncidentDto, CreateIncidentCommand, UpdateIncidentCommand>
{
}
