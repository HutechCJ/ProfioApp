using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Profio.Infrastructure.Persistence.Relational;

public interface IDatabaseFacade
{
  public DatabaseFacade Database { get; }
}
