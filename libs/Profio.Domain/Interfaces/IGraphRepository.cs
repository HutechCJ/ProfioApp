namespace Profio.Domain.Interfaces;

public interface IGraphRepository : IAsyncDisposable
{
  public Task<List<string>> ExecuteReadListAsync(
    string query,
    string returnObjectKey,
    IDictionary<string, object>? parameters = null);

  public  Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(
    string query,
    string returnObjectKey,
    IDictionary<string, object>? parameters = null);

  public Task<T> ExecuteReadScalarAsync<T>(
    string query,
    IDictionary<string, object>? parameters = null);

  public Task<T> ExecuteWriteTransactionAsync<T>(
    string query,
    IDictionary<string, object>? parameters = null);
}
