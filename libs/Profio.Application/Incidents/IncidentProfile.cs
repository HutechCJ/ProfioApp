using Profio.Application.Abstractions.CQRS;
using Profio.Application.Incidents.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Incidents;

public sealed class IncidentProfile : EntityProfileBase<Incident, IncidentDto, CreateIncidentCommand, UpdateIncidentCommand> { }
