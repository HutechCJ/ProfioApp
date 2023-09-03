using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace Profio.Infrastructure.Bus.MQTT.Internal;

internal class MqttClientService : IMqttClientService
{
  private readonly IMqttClient _mqttClient;
  private readonly MqttClientOptions _options;
  private readonly ILogger<MqttClientService> _logger;

  public MqttClientService(MqttClientOptions options, ILogger<MqttClientService> logger)
  {
    _options = options;
    _mqttClient = new MqttFactory().CreateMqttClient();
    _logger = logger;
    ConfigureMqttClient();
  }

  private void ConfigureMqttClient()
  {
    _mqttClient.ConnectedAsync += HandleConnectedAsync;
    _mqttClient.DisconnectedAsync += HandleDisconnectedAsync;
    _mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
  }

  private Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
  {
    // TODO:
    // Deserialize message to Contracts VehicleLocation
    // Serialize to json and send to socket to update the map
    // Calculate the shortest path after 15 minutes, save result to redis cache
    // If vehicle is not moving, send notification to the driver, do not calculate the shortest path
    // If vehicle has incident, send notification to the driver, do not calculate the shortest path
    throw new NotImplementedException();
  }

  private async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
  {
    if (arg.ClientWasConnected)
      await _mqttClient.ConnectAsync(_mqttClient.Options);
  }

  private async Task HandleConnectedAsync(MqttClientConnectedEventArgs arg)
  {
    _logger.LogInformation("The MQTT client is connected.");
    await _mqttClient.SubscribeAsync("/location", MqttQualityOfServiceLevel.AtLeastOnce);
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await _mqttClient.ConnectAsync(_options, cancellationToken);

    _ = Task.Run(
      async () =>
      {
        while (true)
        {
          try
          {
            if (await _mqttClient.TryPingAsync(cancellationToken: cancellationToken))
              continue;

            await _mqttClient.ConnectAsync(_mqttClient.Options, CancellationToken.None);
            _logger.LogInformation("The MQTT client is connected.");
          }
          catch (Exception)
          {
            _logger.LogError("The MQTT client is not connected.");
          }
          finally
          {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
          }
        }
      }, cancellationToken);
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    if (cancellationToken.IsCancellationRequested)
    {
      var disconnectOption = new MqttClientDisconnectOptions
      {
        Reason = (MqttClientDisconnectOptionsReason)MqttClientDisconnectReason.NormalDisconnection,
        ReasonString = "The client is disconnected by the user."
      };
      await _mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
    }
    await _mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
  }
}
