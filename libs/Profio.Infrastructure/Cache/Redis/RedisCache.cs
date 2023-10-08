using System.ComponentModel.DataAnnotations;

namespace Profio.Infrastructure.Cache.Redis;

public sealed class RedisCache
{
  public bool AbortOnConnectFail { get; set; }
  public bool Ssl { get; set; } = true;

  [Range(1, 5, ErrorMessage = "Connect retry must be between 1 and 5.")]
  public byte ConnectRetry { get; set; } = 5;

  [Range(1, 10000, ErrorMessage = "Connect timeout must be between 1 and 10000.")]
  public int ConnectTimeout { get; set; } = 5000;

  [Range(1, 10000, ErrorMessage = "Delta back off must be between 1 and 10000.")]
  public int DeltaBackOff { get; set; } = 1000;

  [Range(1, 3600, ErrorMessage = "Redis default absolute expiration in second must be between 1 and 3600.")]
  public int RedisDefaultSlidingExpirationInSecond { get; set; } = 3600;

  [Range(1, 10000, ErrorMessage = "Sync timeout must be between 1 and 10000.")]
  public int SyncTimeout { get; set; } = 5000;

  public string Password { get; set; } = string.Empty;
  public string Prefix { get; set; } = string.Empty;

  [Required(ErrorMessage = "Redis connection string is required.")]
  [Url(ErrorMessage = "Redis connection string is invalid.")]
  public string Url { get; set; } = string.Empty;

  public string GetConnectionString() =>
    string.IsNullOrEmpty(Password)
      ? Url
      : $"{Url},password={Password},abortConnect=False";
}
