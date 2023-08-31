using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace Profio.Infrastructure.Bus;

public class MQTTPublisher : BackgroundService
{
  private readonly IMqttClient _mqttClient;

  public MQTTPublisher(IMqttClient mqttClient)
  {
    _mqttClient = mqttClient;
  }
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    for (int i = 0; i < 1000; i++)
    {
      var message = new MqttApplicationMessageBuilder()
          .WithTopic("/test/hello")
          .WithPayload($"Hello, MQTT! Message number {i}")
          .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
      .WithRetainFlag()
          .Build();

      await _mqttClient.PublishAsync(message);
      await Task.Delay(1000); // Wait for 1 second
    }
  }
}
