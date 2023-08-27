using Profio.Domain.Graph.Nodes;

namespace Profio.Domain.Interfaces;

public interface IGraphOfWork
{
  public IGraphRepository<Owner> Owner { get; }
}
