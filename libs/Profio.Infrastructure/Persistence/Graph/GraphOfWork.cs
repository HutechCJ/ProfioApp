using Neo4jClient;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Graph;

public class GraphOfWork<T> : IGraphOfWork<T> where T : class
{
  private readonly IBoltGraphClient _client;

  public GraphOfWork(IBoltGraphClient client)
    => _client = client;

  public IGraphRepository<T> Repository => new GraphRepository<T>(_client);
}
