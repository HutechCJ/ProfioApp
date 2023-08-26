using Neo4jClient;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Graph;

public class GraphOfWork : IGraphOfWork, IDisposable
{
  private readonly IGraphClient _neo4JClient;

  public GraphOfWork(IGraphClient neo4JClient) => _neo4JClient = neo4JClient;

  public void Dispose()
  {
    _neo4JClient.Dispose();
    GC.SuppressFinalize(this);
  }
}
