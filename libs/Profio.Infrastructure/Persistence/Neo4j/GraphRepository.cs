using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neo4j.Driver;
using Profio.Domain.Interfaces;

namespace Profio.Infrastructure.Persistence.Neo4j;

public class GraphRepository : IGraphRepository
{
  private readonly IAsyncSession _session;
  private readonly ILogger<GraphRepository> _logger;

  public GraphRepository(
    IDriver driver,
    ILogger<GraphRepository> logger,
    IOptions<ApplicationSettings> appSettingsOption)
  {
    _logger = logger;
    var database = appSettingsOption.Value.InstanceName ?? "neo4j";
    _session = driver.AsyncSession(o => o.WithDatabase(database));
  }

  public async Task<List<string>> ExecuteReadListAsync(
    string query,
    string returnObjectKey,
    IDictionary<string, object>? parameters = null)
    => await ExecuteReadTransactionAsync<string>(query, returnObjectKey, parameters);

  public async Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(
    string query,
    string returnObjectKey,
    IDictionary<string, object>? parameters = null)
    => await ExecuteReadTransactionAsync<Dictionary<string, object>>(query, returnObjectKey, parameters);

  public async Task<T> ExecuteReadScalarAsync<T>(
    string query,
    IDictionary<string, object>? parameters = null)
  {
    try
    {
      parameters ??= new Dictionary<string, object>();

      var result = await _session.ExecuteReadAsync(async tx =>
      {
        var res = await tx.RunAsync(query, parameters);
        var scalar = (await res.SingleAsync())[0].As<T>();
        return scalar;
      });

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "There was a problem while executing database query");
      throw;
    }
  }

  public async Task<T> ExecuteWriteTransactionAsync<T>(
    string query,
    IDictionary<string, object>? parameters = null)
  {
    try
    {
      parameters ??= new Dictionary<string, object>();

      var result = await _session.ExecuteWriteAsync(async tx =>
      {
        var res = await tx.RunAsync(query, parameters);
        var scalar = (await res.SingleAsync())[0].As<T>();
        return scalar;
      });

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "There was a problem while executing database query");
      throw;
    }
  }


  private async Task<List<T>> ExecuteReadTransactionAsync<T>(
    string query,
    string returnObjectKey,
    IDictionary<string, object>? parameters)
  {
    try
    {
      parameters ??= new Dictionary<string, object>();

      var result = await _session.ExecuteReadAsync(async tx =>
      {
        var res = await tx.RunAsync(query, parameters);
        var records = await res.ToListAsync();
        var data = records.Select(x => (T)x.Values[returnObjectKey]).ToList();
        return data;
      });

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "There was a problem while executing database query");
      throw;
    }
  }

  public async ValueTask DisposeAsync()
  {
    await _session.CloseAsync();
    GC.SuppressFinalize(this);
  }
}
