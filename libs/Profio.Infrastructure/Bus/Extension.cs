using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using Profio.Infrastructure.Bus.MQTT;
using Profio.Infrastructure.Bus.MQTT.Internal;
using Profio.Infrastructure.Bus.RabbitMQ;
using Profio.Infrastructure.Bus.RabbitMQ.Internal;
using System.Security.Authentication;

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
        cfg.Host(new Uri(messageQueue.Host), messageQueue.Username, h =>
        {
          h.Username(messageQueue.Username);
          h.Password(messageQueue.Password);
        });
      });
    });

    services.AddTransient<IEventBus, EventBus>();
  }

  public static void AddMqttBus(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<MessageTransport>(configuration.GetSection(nameof(MessageTransport)));

    services.AddSingleton(sp =>
    {
      var transport = sp.GetRequiredService<IOptions<MessageTransport>>().Value;
      var options = new MqttClientOptionsBuilder()
        .WithWebSocketServer(cfg =>
        {
          cfg.WithUri($"wss://{transport.Host}:{transport.Socket}");
          cfg.WithKeepAliveInterval(TimeSpan.FromSeconds(5));
        })
        .WithTcpServer(transport.Host, transport.Mqtt)
        .WithCredentials(transport.UserName, transport.Password)
        .WithClientId($"Profio-{new Random().Next(1, 1000)}")
        .WithTlsOptions(cfg =>
        {
          cfg.UseTls();
          cfg.WithSslProtocols(SslProtocols.Tls12);
          cfg.WithCertificateValidationHandler(_ => true);
        })
        .Build();

      return options;
    });

    services.AddSingleton<MqttClientService>();

    services.AddSingleton<IHostedService>(provider => provider.GetService<MqttClientService>()
                                                      ?? throw new InvalidOperationException());

    services.AddSingleton(serviceProvider =>
    {
      var mqttClientService = serviceProvider.GetService<MqttClientService>();
      var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService ?? throw new InvalidOperationException());
      return mqttClientServiceProvider;
    });

    services.AddSingleton<ExternalService>();
  }
}
