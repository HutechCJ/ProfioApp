using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Profio.Domain.Contracts;
using Profio.Infrastructure.Cache.Redis;
using Profio.Infrastructure.Persistence;

namespace Profio.Infrastructure.Hub;

public sealed class LocationHub : Hub<ILocationClient>
{
  private readonly ILogger<LocationHub> _logger;
  private readonly ApplicationDbContext _context;
  private readonly IRedisCacheService _redisCacheService;

  public LocationHub(
    ILogger<LocationHub> logger,
    ApplicationDbContext context,
    IRedisCacheService redisCacheService)
  {
    _logger = logger;
    _context = context;
    _redisCacheService = redisCacheService;
  }

  public override async Task OnConnectedAsync()
  {
    var httpContext = Context.GetHttpContext();
    var orderId = httpContext!.Request.Query["orderId"];
    if (!string.IsNullOrEmpty(orderId))
    {
      _logger.LogInformation("Client connected: {ConnectionId} - Group {Group}", Context.ConnectionId, orderId);
      await Groups.AddToGroupAsync(Context.ConnectionId, orderId!);
      var vehicleId = await GetVehicleIdForOrderAsync(orderId);
      if (!string.IsNullOrEmpty(vehicleId))
      {
        var latestLocation = _redisCacheService.GetOrSet<VehicleLocation>(
          key: $"latest_location_{vehicleId}",
          valueFactory: () => null!,
          TimeSpan.FromMinutes(10));
        await Clients.Group(orderId!).SendLocation(latestLocation);
      }
    }

    await base.OnConnectedAsync();
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    var httpContext = Context.GetHttpContext();
    var orderId = httpContext!.Request.Query["orderId"];
    if (!string.IsNullOrEmpty(orderId))
    {
      _logger.LogInformation("Client disconnected: {ConnectionId} - Group {Group}", Context.ConnectionId, orderId);
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, orderId!);
    }
    await base.OnDisconnectedAsync(exception);
  }

  private async Task<string?> GetVehicleIdForOrderAsync(StringValues orderId)
  {
    var result = await _context.Deliveries
      .Where(x => x.OrderId == orderId.ToString())
      .OrderByDescending(x => x.DeliveryDate)
      .Select(x => x.Id)
      .FirstOrDefaultAsync();

    return result;
  }

  public async Task SendMessage(VehicleLocation currentLocation)
  {
    var orderIds = await _context.Orders
      .Where(x => x.Deliveries != null && x.Deliveries.Any(d => d.VehicleId == currentLocation.Id))
      .Select(x => x.Id)
      .ToListAsync();
    _logger.LogInformation("Client send message: {ConnectionId}", Context.ConnectionId);
    await Clients.All.SendLocation(currentLocation);
  }
}
