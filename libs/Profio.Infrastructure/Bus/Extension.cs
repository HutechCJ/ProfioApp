using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Profio.Infrastructure.Bus;

public static class Extension
{
  public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<MessageQueue>(configuration.GetSection("MessageBroker"));
    services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageQueue>>().Value);
    services.AddMassTransit(bus =>
    {
      bus.SetKebabCaseEndpointNameFormatter();
      bus.UsingRabbitMq((ctx, cfg) =>
      {
        var messageQueue = ctx.GetRequiredService<MessageQueue>();
        cfg.Host(new Uri(messageQueue.Host), h =>
        {
          h.Username(messageQueue.Username);
          h.Password(messageQueue.Password);
        });
      });
    });
  }
}
