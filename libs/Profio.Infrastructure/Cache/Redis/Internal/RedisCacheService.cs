using Microsoft.Extensions.Options;
using NetCore.AutoRegisterDi;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace Profio.Infrastructure.Cache.Redis.Internal;

[RegisterAsSingleton]
public sealed class RedisCacheService : IRedisCacheService
{
  private const string GetKeysLuaScript = """
                                              local pattern = ARGV[1]
                                              local keys = redis.call('KEYS', pattern)
                                              return keys
                                          """;

  private const string ClearCacheLuaScript = """
                                                 local pattern = ARGV[1]
                                                 for _,k in ipairs(redis.call('KEYS', @pattern)) do
                                                     redis.call('DEL', k)
                                                 end
                                             """;

  private readonly SemaphoreSlim _connectionLock = new(1, 1);

  private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

  private readonly RedisCache _redisCacheOption;

  public RedisCacheService(IOptions<RedisCache> options)
  {
    _redisCacheOption = options.Value;
    _connectionMultiplexer = new(() =>
      ConnectionMultiplexer.Connect(options.Value.GetConnectionString()));
  }

  private ConnectionMultiplexer ConnectionMultiplexer => _connectionMultiplexer.Value;

  private IDatabase Database
  {
    get
    {
      _connectionLock.Wait();

      try
      {
        return ConnectionMultiplexer.GetDatabase();
      }
      finally
      {
        _connectionLock.Release();
      }
    }
  }

  public T GetOrSet<T>(string key, Func<T> valueFactory)
    => GetOrSet($"{_redisCacheOption.Prefix}:{key}", valueFactory,
      TimeSpan.FromSeconds(_redisCacheOption.RedisDefaultSlidingExpirationInSecond));

  public T? Get<T>(string key)
  {
    var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";

    ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

    var cachedValue = Database.StringGet(keyWithPrefix);
    return !string.IsNullOrEmpty(cachedValue)
      ? GetByteToObject<T>(cachedValue)
      : default;
  }

  public T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan expiration)
  {
    var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";

    ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

    var cachedValue = Database.StringGet(keyWithPrefix);
    if (!string.IsNullOrEmpty(cachedValue))
      return GetByteToObject<T>(cachedValue);

    var newValue = valueFactory();
    if (newValue is { })
      Database.StringSet(keyWithPrefix, JsonConvert.SerializeObject(newValue), expiration);

    return newValue;
  }

  public T HashGetOrSet<T>(string key, string hashKey, Func<T> valueFactory)
  {
    ArgumentException.ThrowIfNullOrEmpty(key, nameof(hashKey));
    ArgumentException.ThrowIfNullOrEmpty(hashKey, nameof(hashKey));

    var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";
    var value = Database.HashGet(keyWithPrefix, hashKey.ToLower());

    if (!string.IsNullOrEmpty(value))
      return GetByteToObject<T>(value);

    if (valueFactory() is { })
      Database.HashSet(keyWithPrefix, hashKey.ToLower(),
        JsonConvert.SerializeObject(valueFactory()));
    return valueFactory();
  }

  public IEnumerable<string> GetKeys(string pattern)
    => ((RedisResult[])Database.ScriptEvaluate(GetKeysLuaScript, values: new RedisValue[] { pattern })!)
      .Where(x => x.ToString()!.StartsWith(_redisCacheOption.Prefix))
      .Select(x => x.ToString())
      .ToArray()!;

  public IEnumerable<T> GetValues<T>(string key)
    => Database.HashGetAll($"{_redisCacheOption.Prefix}:{key}").Select(x => GetByteToObject<T>(x.Value));

  public bool RemoveAllKeys(string pattern = "*")
  {
    var succeed = true;

    var keys = GetKeys($"{_redisCacheOption.Prefix}:{pattern}");
    foreach (var key in keys)
      succeed = Database.KeyDelete(key);

    return succeed;
  }

  public void Remove(string key) => Database.KeyDelete($"{_redisCacheOption.Prefix}:{key}");

  public void Reset()
    => Database.ScriptEvaluate(
      ClearCacheLuaScript,
      values: new RedisValue[] { _redisCacheOption.Prefix + "*" },
      flags: CommandFlags.FireAndForget);

  private static T GetByteToObject<T>(RedisValue value)
    => JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value!)) ?? throw new NullReferenceException();
}
