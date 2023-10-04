using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;

namespace Profio.Infrastructure.Persistence.Idempotency;

[RegisterAsScoped]
public sealed class IdempotencyService : IIdempotencyService
{
  private readonly ApplicationDbContext _context;

  public IdempotencyService(ApplicationDbContext context)
    => _context = context;

  public async Task<bool> RequestExistsAsync(Guid id)
    => await _context.Set<IdempotentRequest>().AnyAsync(x => x.Id == id);

  public async Task CreateRequestForCommandAsync(Guid id, string name)
  {
    var request = new IdempotentRequest
    {
      Id = id,
      Name = name,
      CreatedAt = DateTime.UtcNow
    };

    _context.Set<IdempotentRequest>().Add(request);

    await _context.SaveChangesAsync();
  }
}
