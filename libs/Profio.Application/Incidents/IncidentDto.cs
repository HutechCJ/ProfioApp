using Profio.Application.Deliveries;
using Profio.Domain.Constants;
using Profio.Domain.Models;

namespace Profio.Application.Incidents;

public sealed record IncidentDto : BaseModel
{
  public required string Id { get; init; }
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; }
  public DateTime? Time { get; set; }
  public DeliveryDto? Delivery { get; set; }
}
