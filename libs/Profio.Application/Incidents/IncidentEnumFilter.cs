using Profio.Domain.Constants;

namespace Profio.Application.Incidents;

public sealed record IncidentEnumFilter(IncidentStatus? Status);
