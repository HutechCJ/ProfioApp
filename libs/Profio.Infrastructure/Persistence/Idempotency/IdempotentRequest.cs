namespace Profio.Infrastructure.Persistence.Idempotency;

public sealed class IdempotentRequest
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
}
