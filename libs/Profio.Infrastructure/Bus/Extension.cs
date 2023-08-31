using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
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

    services.Configure<MessageTransport>(configuration.GetSection(nameof(MessageTransport)));
    services.AddSingleton<IMqttClient>(sp =>
    {
      var messageTransportSettings = sp.GetRequiredService<IOptions<MessageTransport>>().Value;
      var factory = new MqttFactory();
      var mqttClient = factory.CreateMqttClient();
      var options = new MqttClientOptionsBuilder()
          .WithTcpServer(messageTransportSettings.Host, messageTransportSettings.MQTT)
          .WithCredentials(messageTransportSettings.UserName, messageTransportSettings.Password)
          .WithClientId($"Profio-{new Random().Next(1, 1000)}")
          .WithTls(new MqttClientOptionsBuilderTlsParameters
          {
            UseTls = true,
            SslProtocol = SslProtocols.Tls12,
            CertificateValidationHandler = delegate { return true; },
          })
          .Build();
      mqttClient.ConnectAsync(options, CancellationToken.None).Wait();
      return mqttClient;
    });
    // Test MqttClient
    //services.AddHostedService<MQTTPublisher>();
  }
}
