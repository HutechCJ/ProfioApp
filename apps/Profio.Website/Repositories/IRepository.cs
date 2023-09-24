namespace Profio.Website.Repositories;

public interface IRepository
{
  Task<TResult?> GetAsync<TResult>(string route);
  Task<TResult?> PostAsync<TResult>(string route, object? body);
  Task<TResult?> PatchAsync<TResult>(string route, object? body);
  Task<TResult?> PutAsync<TResult>(string route, object? body);
  Task<TResult?> DeleteAsync<TResult>(string route);
}
