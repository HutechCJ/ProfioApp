using Microsoft.Extensions.Configuration;

namespace Profio.Infrastructure.Key;

public class ApiKeyValidation : IApiKeyValidation
{
  private readonly IConfiguration _configuration;

  public ApiKeyValidation(IConfiguration configuration)
    => _configuration = configuration;

  public bool IsValidApiKey(string userApiKey)
  {
    if (string.IsNullOrWhiteSpace(userApiKey))
      return false;

    var apiKey = _configuration.GetValue<string>(ApiKey.ApiKeyName);

    return apiKey is { } && apiKey == userApiKey;
  }
}
