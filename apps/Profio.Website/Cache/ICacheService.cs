namespace Profio.Website.Cache;

public interface ICacheService
{
  public Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getItemCallback);
  public void Remove(string cacheKey);
}
