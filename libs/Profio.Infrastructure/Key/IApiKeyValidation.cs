namespace Profio.Infrastructure.Key;

public interface IApiKeyValidation
{
  public bool IsValidApiKey(string userApiKey);
}
