namespace Profio.Infrastructure.Persistence.Idempotency;

public interface IIdempotencyService
{
  public Task<bool> RequestExistsAsync(Guid id);
  public Task CreateRequestForCommandAsync(Guid id, string name);
}
