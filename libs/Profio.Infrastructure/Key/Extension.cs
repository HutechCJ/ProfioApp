using Microsoft.Extensions.DependencyInjection;

namespace Profio.Infrastructure.Key;

public static class Extension
{
  public static void AddApiKey(this IServiceCollection services)
  {
    services.AddScoped<ApiKeyAuthFilter>();
    services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
  }
}
