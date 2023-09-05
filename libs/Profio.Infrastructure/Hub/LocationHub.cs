using Microsoft.Extensions.Logging;

namespace Profio.Infrastructure.Hub;

public sealed class LocationHub : Microsoft.AspNetCore.SignalR.Hub<ILocationClient>
{
  private readonly ILogger<LocationHub> _logger;

  public LocationHub(ILogger<LocationHub> logger)
    => _logger = logger;

  public override async Task OnConnectedAsync()
  {
    _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
    await base.OnConnectedAsync();
  }

  public async Task SendMessage(string currentLocation)
  {
    _logger.LogInformation("Client send message: {ConnectionId}", Context.ConnectionId);
    await Clients.All.SendLocation(currentLocation);
  }
}
