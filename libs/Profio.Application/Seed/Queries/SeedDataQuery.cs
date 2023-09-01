using System.Text.Json;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Infrastructure.Persistence;
using Serilog;

namespace Profio.Application.Seed.Queries
{
  public record SeedDataQuery : IRequest<string>;
  public class SeedDataHandler : IRequestHandler<SeedDataQuery, string>
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    public SeedDataHandler(IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
      _unitOfWork = unitOfWork;
      _context = context;
    }
    public async Task<string> Handle(SeedDataQuery request, CancellationToken cancellationToken)
    {
      await _context.Hubs.ExecuteDeleteAsync();

      await HubSeeding();
      // await RouteSeeding();
      return "Seeding Success";
    }

    private async Task RouteSeeding()
    {
      if (!await _context.Routes.AnyAsync())
      {
        string json = File.ReadAllText(PathSeed.RouteData);
        List<Route> routes = JsonSerializer.Deserialize<List<Route>>(json)!;
        await _context.AddRangeAsync(routes);
        await _context.SaveChangesAsync();

        var routeList = await _context.Routes.ToListAsync();
        Log.Information("Added route logging" + JsonSerializer.Serialize(routeList));
      };
    }

    private async Task HubSeeding()
    {
      if (!await _context.Hubs.AnyAsync())
      {
        string json = File.ReadAllText(PathSeed.HubData);
        List<Hub> hubs = JsonSerializer.Deserialize<List<Hub>>(json)!;
        await _context.AddRangeAsync(hubs);
        await _context.SaveChangesAsync();

        var hubList = await _context.Hubs.ToListAsync();
        Log.Information("Added hub logging" + JsonSerializer.Serialize(hubList));
      };
    }
  }
}
