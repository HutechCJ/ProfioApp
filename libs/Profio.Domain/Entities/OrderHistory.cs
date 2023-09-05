namespace Profio.Domain.Entities;

public class OrderHistory
{
  public required string? Id { get; set; } = Ulid.NewUlid().ToString();
  public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
  public string? OrderId { get; set; }
  public Order? Order { get; set; }
  public string? VehicleId { get; set; }
  public Vehicle? Vehicle { get; set; }
  public string? HubId { get; set; }
  public Hub? Hub { get; set; }
  public ICollection<Incident>? Incidents { get; set; } = new List<Incident>();
}
