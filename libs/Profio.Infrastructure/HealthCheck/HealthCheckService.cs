namespace Profio.Infrastructure.HealthCheck;

public sealed class HealthService
{
  public bool IsHealthy { get; private set; } = true;
}
