namespace Profio.Infrastructure.Persistence.Idempotency;

public interface IIdempotencyService
{
  Task<bool> RequestExistsAsync(Guid id);
  Task CreateRequestForCommandAsync(Guid id, string name);
}
