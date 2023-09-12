using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Profio.Infrastructure.Email.FluentEmail;
using Profio.Infrastructure.Email.FluentEmail.Internal;

namespace Profio.Infrastructure.Email;

public static class Extension
{
  public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<FluentEmail.Email>(configuration.GetSection(nameof(FluentEmail)));
    var cfg = services.BuildServiceProvider().GetRequiredService<IOptions<FluentEmail.Email>>().Value;
    services.AddFluentEmail(cfg.From)
      .AddSendGridSender(cfg.SendGridApiKey)
      .AddRazorRenderer();
    services.AddScoped<IEmailService, EmailService>();
    return services;
  }
}
