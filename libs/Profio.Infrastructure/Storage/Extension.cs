using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Profio.Infrastructure.Storage.Supabase;
using Profio.Infrastructure.Storage.Supabase.Internals;
using Supabase;

namespace Profio.Infrastructure.Storage;

public static class Extension
{
  public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<Supabase.Supabase>(configuration.GetSection(nameof(Supabase)));
    var cfg = services.BuildServiceProvider().GetRequiredService<IOptions<Supabase.Supabase>>().Value;
    services.AddScoped<Client>(_ => new(
      cfg.Url ?? throw new InvalidOperationException(),
      cfg.Key,
      new()
      {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
      }
    ));
    services.AddScoped<IStorageService, StorageService>();
  }
}
