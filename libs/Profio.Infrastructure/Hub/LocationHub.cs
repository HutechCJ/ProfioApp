namespace Profio.Infrastructure.Hub;

public sealed class LocationHub : Microsoft.AspNetCore.SignalR.Hub<ILocationClient>
{
  public override async Task OnConnectedAsync()
    => await base.OnConnectedAsync();

  public async Task SendLocation(string currentLocation)
    => await Clients.All.SendLocation(currentLocation);
}
