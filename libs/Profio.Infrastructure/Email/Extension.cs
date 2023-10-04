using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Profio.Infrastructure.Email;

public static class Extension
{
  public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<FluentEmail.Email>(configuration.GetSection(nameof(FluentEmail.Email)));
    var cfg = services.BuildServiceProvider().GetRequiredService<IOptions<FluentEmail.Email>>().Value;
    services.AddFluentEmail(cfg.From, "CJ Logistics")
      .AddSmtpSender(cfg.Host, cfg.Port, cfg.Username, cfg.Password)
      .AddLiquidRenderer();
    return services;
  }
}
