using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using Profio.Domain.Contracts;
using Profio.Infrastructure.Cache.Redis;
using Profio.Infrastructure.Hub;
using System.Text.Json;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Profio.Infrastructure.Bus.MQTT.Internal;

public sealed class MqttClientService : IMqttClientService
{
  private readonly IMqttClient _mqttClient;
  private readonly MqttClientOptions _options;
  private readonly ILogger<MqttClientService> _logger;
  private readonly IHubContext<LocationHub, ILocationClient> _context;
  private readonly IRedisCacheService _redisCacheService;
  private readonly Timer _locationSendTimer;
  private readonly Dictionary<string, VehicleLocation> _latestVehicleLocations = new();
  private readonly SemaphoreSlim _lockObject = new(1, 1);

  public MqttClientService(
    MqttClientOptions options,
    ILogger<MqttClientService> logger,
    IHubContext<LocationHub, ILocationClient> context,
    IRedisCacheService redisCacheService)
  {
    _options = options;
    _mqttClient = new MqttFactory().CreateMqttClient();
    _logger = logger;
    _context = context;
    _redisCacheService = redisCacheService;
    _locationSendTimer = new Timer(10000);
    _locationSendTimer.Elapsed += HandleLocationSendTimerElapsed;
    _locationSendTimer.Start();

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
    var payload = arg.ApplicationMessage.ConvertPayloadToString();
    var jsonSerializeOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    var location = JsonSerializer.Deserialize<VehicleLocation>(payload, jsonSerializeOptions);
    if (location is null)
      return;

    await _lockObject.WaitAsync();

    try
    {
      if (string.IsNullOrEmpty(location.Id))
        return;
      _latestVehicleLocations[location.Id] = location;
      _redisCacheService.Remove($"latest_location_{location.Id}");
      _redisCacheService.GetOrSet($"latest_location_{location.Id}", () => location, TimeSpan.FromMinutes(10));
    }
    finally
    {
      _lockObject.Release();
    }
  }

  private async void HandleLocationSendTimerElapsed(object? sender, ElapsedEventArgs e)
  {
    await SendLocation();
  }

  private async Task SendLocation()
  {
    var tasks = new List<Task>();
    await _lockObject.WaitAsync();

    try
    {
      tasks.AddRange(from location in _latestVehicleLocations.Values from orderId in location.OrderIds select _context.Clients.Group(orderId).SendLocation(location));

      _latestVehicleLocations.Clear();
    }
    finally
    {
      _lockObject.Release();
    }

    await Task.WhenAll(tasks);
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
