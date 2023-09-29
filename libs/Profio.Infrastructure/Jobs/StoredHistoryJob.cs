using Profio.Domain.Contracts;
using Profio.Infrastructure.Cache.Redis.Internal;
using Profio.Infrastructure.Persistence;
using Quartz;

namespace Profio.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class StoredHistoryJob : IJob
{
  private readonly RedisCacheService _redisCacheService;
  private readonly ApplicationDbContext _context;

  public StoredHistoryJob(RedisCacheService redisCacheService, ApplicationDbContext context)
    => (_redisCacheService, _context) = (redisCacheService, context);

  public Task Execute(IJobExecutionContext context)
  {
    var keys = _redisCacheService.GetKeys("stored_location_*");

    return Task.CompletedTask;
  }
}
