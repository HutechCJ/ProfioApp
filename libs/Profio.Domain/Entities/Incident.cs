using Profio.Domain.Constants;

namespace Profio.Domain.Entities;

public class Incident : BaseEntity
{
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; } = IncidentStatus.InProgress;
  public DateTime? Time { get; set; }
  public string? OrderHistoryId { get; set; }
  public OrderHistory? OrderHistory { get; set; }
}
