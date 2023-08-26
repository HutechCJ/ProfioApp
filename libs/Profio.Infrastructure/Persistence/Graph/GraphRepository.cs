using System.Linq.Expressions;
using Neo4jClient;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Graph;

public class GraphRepository<T> : IGraphRepository<T> where T : class
{
  private readonly IGraphClient _graphClient;

  public GraphRepository(IGraphClient graphClient)
    => _graphClient = graphClient;

  public async Task<List<T>> GetAsync(string query)
  {
    var result = await _graphClient.Cypher
      .Match(query)
      .Return(n => n.As<T>())
      .ResultsAsync;

    return result.ToList();
  }

  public async Task<List<T>> GetByParam(string query, Dictionary<string, object> parameters)
  {
    var result = await _graphClient.Cypher
      .Match(query)
      .WithParams(parameters)
      .Return(n => n.As<T>())
      .ResultsAsync;

    return result.ToList();
  }

  public async Task<T> GetOne(string query, Expression<Func<T, bool>> predicate)
  {
    var result = await _graphClient.Cypher
      .Match(query)
      .Where(predicate)
      .Return(n => n.As<T>())
      .ResultsAsync;

    return result.FirstOrDefault() ?? throw new("No record found");
  }

  public async Task CreateAsync(string query, T node)
    => await _graphClient.Cypher
      .Create(query)
      .WithParam("node", node)
      .ExecuteWithoutResultsAsync();

  public async Task UpdateAsync(string query, T node)
    => await _graphClient.Cypher
      .Match(query)
      .Set("n = $node")
      .WithParam("node", node)
      .ExecuteWithoutResultsAsync();

  public async Task DeleteAsync(string query)
    => await _graphClient.Cypher
      .Match(query)
      .Delete("n")
      .ExecuteWithoutResultsAsync();
}
