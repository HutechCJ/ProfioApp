namespace Profio.Domain.Interfaces;

public interface IGraphOfWork<T> where T : class
{
  public IGraphRepository<T> Repository { get; }
}
