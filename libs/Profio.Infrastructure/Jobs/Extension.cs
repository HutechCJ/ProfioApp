using Microsoft.AspNetCore.Builder;
using Quartz;

namespace Profio.Infrastructure.Jobs;

public static class Extension
{
  public static void AddBackgroundJob(this WebApplicationBuilder builder)
  {
    builder.Services.AddQuartz(options =>
      {
        var jobKey = new JobKey(nameof(StoredHistoryJob));

        options.AddJob<StoredHistoryJob>(jobKey)
          .AddTrigger(
            trigger => trigger
              .ForJob(jobKey)
              .WithSimpleSchedule(schedule => schedule
                .WithIntervalInMinutes(25)
                .RepeatForever()
              )
          );
      });

    builder.Services.AddQuartzHostedService(options =>
      options.WaitForJobsToComplete = true
    );
  }

}
