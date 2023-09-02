using Profio.Domain.Constants;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Hubs;

public class HubDto
{
  public required string? Name { get; set; }
  public required string? ZipCode { get; set; }
  public Location? Location { get; set; }
  public Address? Address { get; set; }
  public HubStatus Status { get; set; } = HubStatus.Active;
}
