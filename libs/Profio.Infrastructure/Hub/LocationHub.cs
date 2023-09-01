namespace Profio.Infrastructure.Hub;

public class LocationHub : Microsoft.AspNetCore.SignalR.Hub<ILocationClient>
{
  public override async Task OnConnectedAsync()
    => await base.OnConnectedAsync();

  public async Task SendMessage(string currentLocation)
    => await Clients.All.SendLocation(currentLocation);
}
