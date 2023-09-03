namespace Profio.Infrastructure.Bus.MQTT.Internal;

public class MqttClientServiceProvider
{
  public readonly IMqttClientService MqttClientService;

  public MqttClientServiceProvider(IMqttClientService mqttClientService)
    => MqttClientService = mqttClientService;
}
