using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Profio.Infrastructure.Persistence;

public interface IDatabaseFacade
{
  public DatabaseFacade Database { get; }
}
