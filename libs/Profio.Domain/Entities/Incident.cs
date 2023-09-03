using Profio.Domain.Constants;
using Profio.Domain.Interfaces;

namespace Profio.Domain.Entities;

public class Incident : IEntity<string>
{
  public string Id { get; set; } = Ulid.NewUlid().ToString();
  public string? Description { get; set; }
  public IncidentStatus Status { get; set; } = IncidentStatus.InProgress;
  public DateTime? Time { get; set; }
  public string? OrderHistoryId { get; set; }
  public OrderHistory? OrderHistory { get; set; }
}
