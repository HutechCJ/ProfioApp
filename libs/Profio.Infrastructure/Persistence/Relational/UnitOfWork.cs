using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Relational;

public class UnitOfWork<T> : IAsyncDisposable, IUnitOfWork<T> where T : class
{
  private readonly ApplicationDbContext _context;

  public UnitOfWork(ApplicationDbContext context)
    => _context = context;

  public IRelationalRepository<T> Repository => new RelationalRepository<T>(_context);

  public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    => _context.SaveChangesAsync(cancellationToken);

  public async ValueTask DisposeAsync()
  {
    await _context.DisposeAsync();
    GC.SuppressFinalize(this);
  }
}
