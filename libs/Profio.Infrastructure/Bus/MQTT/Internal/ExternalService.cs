namespace Profio.Infrastructure.Bus.MQTT.Internal;

public sealed class ExternalService
{
  private readonly IMqttClientService _mqttClientService;

  public ExternalService(MqttClientServiceProvider provider)
    => _mqttClientService = provider.MqttClientService;
}
