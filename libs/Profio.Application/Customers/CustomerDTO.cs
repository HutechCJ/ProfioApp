using Profio.Domain.Constants;
using Profio.Domain.Models;
using Profio.Domain.ValueObjects;

namespace Profio.Application.Customers;

public record CustomerDto : BaseModel
{
  public required string Id { get; init; }
  public required string? Name { get; init; }
  public required string? Phone { get; init; }
  public string? Email { get; init; }
  public Gender? Gender { get; init; }
  public required Address? Address { get; init; }
}
