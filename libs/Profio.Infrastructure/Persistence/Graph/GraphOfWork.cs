using Neo4jClient;
using Profio.Domain.Graph.Nodes;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Graph;

public class GraphOfWork : IGraphOfWork, IDisposable
{
  private IGraphRepository<Owner>? _owner;
  private readonly IBoltGraphClient _neo4JClient;

  public GraphOfWork(IBoltGraphClient neo4JClient) => _neo4JClient = neo4JClient;

  public IGraphRepository<Owner> Owner 
    => _owner ??= new GraphRepository<Owner>(_neo4JClient);

  public void Dispose()
  {
    _neo4JClient.Dispose();
    GC.SuppressFinalize(this);
  }
}
