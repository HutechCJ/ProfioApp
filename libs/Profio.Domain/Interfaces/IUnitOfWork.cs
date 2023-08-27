namespace Profio.Domain.Interfaces;

public interface IUnitOfWork<T> where T : class
{
  public IRelationalRepository<T> Repository { get; }
}
