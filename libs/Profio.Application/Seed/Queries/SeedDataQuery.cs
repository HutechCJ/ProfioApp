using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Seed.Queries;

public record SeedDataQuery : IRequest<string>;

public class SeedDataHandler : IRequestHandler<SeedDataQuery, string>
{
  private readonly ApplicationDbContext _context;
  private readonly ILogger<SeedDataHandler> _logger;

  public SeedDataHandler(ApplicationDbContext context, ILogger<SeedDataHandler> logger)
    => (_context, _logger) = (context, logger);

  public async Task<string> Handle(SeedDataQuery request, CancellationToken cancellationToken)
  {
    await HubSeeding();
    await RouteSeeding();
    await CustomerSeeding();
    await OrderSeeding();
    await VehicleSeeding();
    await StaffSeeding();
    return "Seeding Success";
  }

  private async Task RouteSeeding()
  {
    if (!await _context.Routes.AnyAsync())
    {
      var json = await File.ReadAllTextAsync(PathSeed.RouteData);
      var routes = JsonSerializer.Deserialize<List<Route>>(json)!;
      await _context.AddRangeAsync(routes);
      await _context.SaveChangesAsync();
      var routeList = await _context.Routes.ToListAsync();
      _logger.LogInformation("Added route logging {routeList}",  JsonSerializer.Serialize(routeList));
    }
  }

  private async Task HubSeeding()
  {
    if (!await _context.Hubs.AnyAsync())
    {
      var json = await File.ReadAllTextAsync(PathSeed.HubData);
      var hubs = JsonSerializer.Deserialize<List<Hub>>(json)!;
      await _context.AddRangeAsync(hubs);
      await _context.SaveChangesAsync();
      var hubList = await _context.Hubs.ToListAsync();
      _logger.LogInformation("Added hub logging {hubList}", JsonSerializer.Serialize(hubList));
    }
  }

  private async Task CustomerSeeding()
  {
    if (!await _context.Routes.AnyAsync())
    {
      var json = await File.ReadAllTextAsync(PathSeed.CustomerData);
      var customers = JsonSerializer.Deserialize<List<Customer>>(json)!;
      await _context.AddRangeAsync(customers);
      await _context.SaveChangesAsync();
      var customerList = await _context.Customers.ToListAsync();
      _logger.LogInformation("Added customer logging {customerList}", JsonSerializer.Serialize(customerList));
    }
  }

  private async Task OrderSeeding()
  {
    var json = await File.ReadAllTextAsync(PathSeed.OrderData);
    var orders = JsonSerializer.Deserialize<List<Order>>(json)!;
    await _context.AddRangeAsync(orders);
    await _context.SaveChangesAsync();
    var orderList = await _context.Orders.ToListAsync();
    _logger.LogInformation("Added order logging {orderList}", JsonSerializer.Serialize(orderList));
  }

  private async Task VehicleSeeding()
  {
    var json = await File.ReadAllTextAsync(PathSeed.VehicleData);
    var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json)!;
    await _context.AddRangeAsync(vehicles);
    await _context.SaveChangesAsync();
    var vehicleList = await _context.Vehicles.ToListAsync();
    _logger.LogInformation("Added vehicle logging {vehicleList}", JsonSerializer.Serialize(vehicleList));
  }

  private async Task StaffSeeding()
  {
    var json = await File.ReadAllTextAsync(PathSeed.StaffData);
    var staffs = JsonSerializer.Deserialize<List<Staff>>(json)!;
    await _context.AddRangeAsync(staffs);
    await _context.SaveChangesAsync();
    var staffList = await _context.Staffs.ToListAsync();
    _logger.LogInformation("Added staff logging {staffList}", JsonSerializer.Serialize(staffList));
  }
}
