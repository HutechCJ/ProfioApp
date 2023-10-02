namespace Profio.Website.Repositories;

public interface IRepository
{
  public Task<TResult?> GetAsync<TResult>(string route);
  public Task<TResult?> PostAsync<TResult>(string route, object? body);
  public Task<TResult?> PatchAsync<TResult>(string route, object? body);
  public Task<TResult?> PutAsync<TResult>(string route, object? body);
  public Task<TResult?> DeleteAsync<TResult>(string route);
}
