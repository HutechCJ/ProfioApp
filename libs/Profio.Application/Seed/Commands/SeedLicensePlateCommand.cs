using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Seed.Commands;

public sealed record SeedLicensePlateCommand : IRequest<string>;

public sealed class SeedLicensePlateCommandHandler : IRequestHandler<SeedLicensePlateCommand, string>
{
  private readonly ApplicationDbContext _context;
  private readonly ILogger<SeedLicensePlateCommandHandler> _logger;

  public SeedLicensePlateCommandHandler(ApplicationDbContext context, ILogger<SeedLicensePlateCommandHandler> logger)
    => (_context, _logger) = (context, logger);

  public Task<string> Handle(SeedLicensePlateCommand request, CancellationToken cancellationToken)
  {
    var vehiclesToUpdate = _context.Vehicles;

    foreach (var vehicle in vehiclesToUpdate)
      _context.Vehicles
        .Where(x => x.Id == vehicle.Id)
        .ExecuteUpdate(x => x.SetProperty(
          v => v.LicensePlate,
          p => GenerateLicensePlate()
        ));

    return Task.FromResult("Seed License Plate Success!");
  }

  public string GenerateLicensePlate()
  {
    Random random = new();

    var provinceNumber = random.Next(11, 99);
    var districtCode = (char)random.Next('A', 'Z' + 1);
    var uniqueNumber = random.Next(0, 100000);

    var licensePlate = $"{provinceNumber}{districtCode}-{uniqueNumber:D5}";

    _logger.LogInformation("Random Vietnamese License Plate: {LicensePlate}", licensePlate);

    return licensePlate;
  }
}
