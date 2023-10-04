using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Cache.Redis;
using Profio.Infrastructure.Cache.Redis.Internal;
using StackExchange.Redis;

namespace Profio.Infrastructure.Cache;

public static class Extension
{
  public static IServiceCollection AddRedisCache(
    this IServiceCollection services,
    IConfiguration config,
    Action<RedisCache>? setupAction = null)
  {
    ArgumentNullException.ThrowIfNull(services, nameof(services));

    if (services.Contains(ServiceDescriptor.Singleton<IRedisCacheService, RedisCacheService>()))
      return services;

    var redisCacheOption = new RedisCache();
    var redisCacheSection = config.GetSection(nameof(RedisCache));
    redisCacheSection.Bind(redisCacheOption);
    services.Configure<RedisCache>(redisCacheSection);
    setupAction?.Invoke(redisCacheOption);

    services.AddStackExchangeRedisCache(options =>
    {
      options.ConfigurationOptions = GetRedisConfigurationOptions(redisCacheOption, config);
      options.InstanceName = config[redisCacheOption.Prefix];
    });

    return services;
  }

  private static ConfigurationOptions GetRedisConfigurationOptions(
    RedisCache redisCacheOption,
    IConfiguration config)
  {
    var configurationOptions = new ConfigurationOptions
    {
      ConnectTimeout = redisCacheOption.ConnectTimeout,
      SyncTimeout = redisCacheOption.SyncTimeout,
      ConnectRetry = redisCacheOption.ConnectRetry,
      AbortOnConnectFail = redisCacheOption.AbortOnConnectFail,
      ReconnectRetryPolicy = new ExponentialRetry(redisCacheOption.DeltaBackOff),
      KeepAlive = 5,
      Ssl = redisCacheOption.Ssl
    };

    if (!string.IsNullOrEmpty(redisCacheOption.Password))
      configurationOptions.Password = redisCacheOption.Password;

    redisCacheOption.Url = config
      .GetConnectionString("Redis") ?? throw new InvalidOperationException();

    var endpoints = redisCacheOption.Url.Split(':');
    foreach (var endpoint in endpoints)
      configurationOptions.EndPoints.Add(endpoint);

    return configurationOptions;
  }
}
