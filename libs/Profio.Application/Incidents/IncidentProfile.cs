using Profio.Application.CQRS;
using Profio.Application.Incidents.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Incidents;

public class IncidentProfile : EntityProfileBase<Incident, IncidentDto, CreateIncidentCommand, UpdateIncidentCommand> { }
