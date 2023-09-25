using Microsoft.Extensions.Caching.Memory;

namespace Profio.Website.Cache;

public class CacheService : ICacheService
{
  private readonly IMemoryCache _cache;
  private readonly MemoryCacheEntryOptions _cacheDuration;

  public CacheService(IMemoryCache cache)
  {
    _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    _cacheDuration = new()
    {
      AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
      SlidingExpiration = TimeSpan.FromSeconds(30)
    };
  }

  public async Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getItemCallback)
  {
    ArgumentException.ThrowIfNullOrEmpty(cacheKey, nameof(cacheKey));

    if (_cache.TryGetValue(cacheKey, out T? cachedItem))
      return cachedItem!;

    var newItem = await getItemCallback();

    if (newItem is null)
      return newItem;

    _cache.Set(cacheKey, newItem, _cacheDuration);

    return newItem;
  }

  public void Remove(string cacheKey)
  {
    ArgumentException.ThrowIfNullOrEmpty(cacheKey, nameof(cacheKey));
    _cache.Remove(cacheKey);
  }
}
