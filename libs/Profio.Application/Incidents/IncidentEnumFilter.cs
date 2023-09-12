using Profio.Domain.Constants;

namespace Profio.Application.Incidents;

public record IncidentEnumFilter(IncidentStatus? Status);
