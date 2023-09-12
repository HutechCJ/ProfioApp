using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using Profio.Infrastructure.Hub;

namespace Profio.Infrastructure.Bus.MQTT.Internal;

public sealed class MqttClientService : IMqttClientService
{
  private readonly IMqttClient _mqttClient;
  private readonly MqttClientOptions _options;
  private readonly IHubContext<LocationHub, ILocationClient> _context;
  private readonly ILogger<MqttClientService> _logger;

  public MqttClientService(
    MqttClientOptions options,
    ILogger<MqttClientService> logger,
    IHubContext<LocationHub, ILocationClient> context)
  {
    _options = options;
    _mqttClient = new MqttFactory().CreateMqttClient();
    _logger = logger;
    _context = context;
    ConfigureMqttClient();
  }

  private void ConfigureMqttClient()
  {
    _mqttClient.ConnectedAsync += HandleConnectedAsync;
    _mqttClient.DisconnectedAsync += HandleDisconnectedAsync;
    _mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
  }

  private async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
  {
    _logger.LogInformation("The MQTT client received a message: {Message}", arg.ApplicationMessage.ConvertPayloadToString());
    await _context.Clients.All.SendLocation(arg.ApplicationMessage.ConvertPayloadToString());
  }

  private async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
  {
    if (arg.ClientWasConnected)
      await _mqttClient.ConnectAsync(_mqttClient.Options);
  }

  private async Task HandleConnectedAsync(MqttClientConnectedEventArgs arg)
  {
    _logger.LogInformation("The MQTT client subscribed to topic: /location");
    await _mqttClient.SubscribeAsync("/location", cancellationToken: CancellationToken.None);
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
          catch (Exception ex)
          {
            _logger.LogError(ex, "The MQTT client is disconnected.");
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
