namespace Profio.Website.Cache;

public interface ICacheService
{
  Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getItemCallback);
  void Remove(string cacheKey);
}
