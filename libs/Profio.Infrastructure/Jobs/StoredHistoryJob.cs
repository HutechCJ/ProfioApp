using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Profio.Domain.Contracts;
using Profio.Domain.Entities;
using Profio.Infrastructure.Cache.Redis;
using Profio.Infrastructure.Persistence;
using Quartz;

namespace Profio.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class StoredHistoryJob : IJob
{
  private readonly IRedisCacheService _redisCacheService;
  private readonly ApplicationDbContext _context;
  private readonly ILogger<StoredHistoryJob> _logger;
  private readonly IOptions<RedisCache> _options;

  public StoredHistoryJob(IRedisCacheService redisCacheService, ApplicationDbContext context,
    ILogger<StoredHistoryJob> logger, IOptions<RedisCache> options)
    => (_redisCacheService, _context, _logger, _options) = (redisCacheService, context, logger, options);

  public async Task Execute(IJobExecutionContext context)
  {
    var keys = _redisCacheService.GetKeys("*stored_location_*");
    var enumerable = keys as string[] ?? keys.ToArray();
    _logger.LogInformation("The job executed: {Message} - {Keys}", nameof(StoredHistoryJob), string.Join(", ", enumerable));
    if (!enumerable.Any()) return;

    foreach (var key in enumerable)
    {
      var keyWithoutPrefix = key[(_options.Value.Prefix.Length + 1)..];
      var location = _redisCacheService.Get<VehicleLocation>(
        key: keyWithoutPrefix);

      if (location is null) return;

      var deliveryProcesses = location.OrderIds.Select(orderId
        => new DeliveryProgress
        {
          CurrentLocation = new(location.Latitude!.Value, location.Longitude!.Value),
          OrderId = orderId,
          LastUpdated = DateTime.UtcNow,
        }).ToList();

      await _context.DeliveryProgresses.AddRangeAsync(deliveryProcesses);

      if (deliveryProcesses.Count <= 0) continue;
      await _context.SaveChangesAsync();
      _redisCacheService.Remove(key[(_options.Value.Prefix.Length + 1)..]);
    }
  }
}
