using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;
using System.Text.Json;

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
    await StaffSeeding();
    await VehicleSeeding();
    await OrderSeeding();
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
      _logger.LogInformation("Added route logging {routeList}", JsonSerializer.Serialize(routeList));
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
    if (!await _context.Customers.AnyAsync())
    {
      var json = await File.ReadAllTextAsync(PathSeed.CustomerData);
      var customers = JsonSerializer.Deserialize<List<Customer>>(json)!;
      var hubZipCodes = _context.Hubs.Select(x => x.ZipCode).ToList();
      foreach (var customer in customers)
      {
        var zipCode = hubZipCodes[new Random().Next(0, hubZipCodes.Count)];
        if (customer.Address != null)
        {
          customer.Address.ZipCode = zipCode;
        }
      }
      await _context.AddRangeAsync(customers);
      await _context.SaveChangesAsync();
      var customerList = await _context.Customers.ToListAsync();
      _logger.LogInformation("Added customer logging {customerList}", JsonSerializer.Serialize(customerList));
    }
  }

  private async Task OrderSeeding()
  {
    if (!await _context.Orders.AnyAsync())
    {
      var json = await File.ReadAllTextAsync(PathSeed.OrderData);
      var orders = JsonSerializer.Deserialize<List<Order>>(json)!;
      var customerIds = _context.Customers.Select(x => x.Id).ToList();
      var hubZipCodes = _context.Hubs.Select(x => x.ZipCode).ToList();
      foreach (var order in orders)
      {
        order.CustomerId = customerIds[new Random().Next(0, customerIds.Count)];
        var zipCode = hubZipCodes[new Random().Next(0, hubZipCodes.Count)];
        order.DestinationZipCode = zipCode;
        if (order.DestinationZipCode != null)
        {
          order.DestinationAddress!.ZipCode = zipCode;
        }
      }
      await _context.AddRangeAsync(orders);
      await _context.SaveChangesAsync();
      var orderList = await _context.Orders.ToListAsync();
      _logger.LogInformation("Added order logging {orderList}", JsonSerializer.Serialize(orderList));
    }
  }

  private async Task VehicleSeeding()
  {
    if (!await _context.Orders.AnyAsync())
    {
      var json = await File.ReadAllTextAsync(PathSeed.VehicleData);
      var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json)!;
      var staffIds = _context.Staffs.Select(x => x.Id).ToList();
      foreach (var vehicle in vehicles)
      {
        vehicle.StaffId = staffIds[new Random().Next(0, staffIds.Count)];
      }
      await _context.AddRangeAsync(vehicles);
      await _context.SaveChangesAsync();
      var vehicleList = await _context.Vehicles.ToListAsync();
      _logger.LogInformation("Added vehicle logging {vehicleList}", JsonSerializer.Serialize(vehicleList));
    }
  }

  private async Task StaffSeeding()
  {
    if (!await _context.Staffs.AnyAsync())
    {
      var json = await File.ReadAllTextAsync(PathSeed.StaffData);
      var staffs = JsonSerializer.Deserialize<List<Staff>>(json)!;
      await _context.AddRangeAsync(staffs);
      await _context.SaveChangesAsync();
      var staffList = await _context.Staffs.ToListAsync();
      _logger.LogInformation("Added staff logging {staffList}", JsonSerializer.Serialize(staffList));
    }
  }
}
