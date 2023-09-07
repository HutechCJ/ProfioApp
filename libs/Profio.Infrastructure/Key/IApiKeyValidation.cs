namespace Profio.Infrastructure.Key;

public interface IApiKeyValidation
{
  bool IsValidApiKey(string userApiKey);
}
