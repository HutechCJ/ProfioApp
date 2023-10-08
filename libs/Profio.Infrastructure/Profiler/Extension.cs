using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling.Storage;

namespace Profio.Infrastructure.Profiler;

public static class Extension
{
  public static WebApplicationBuilder AddProfiler(this WebApplicationBuilder builder)
  {
    builder.Services.AddMiniProfiler(options =>
    {
      options.RouteBasePath = "/profiler";
      ((options.Storage as MemoryCacheStorage)!).CacheDuration = TimeSpan.FromMinutes(60);
      options.TrackConnectionOpenClose = true;
      options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;
      options.EnableMvcFilterProfiling = true;
      options.EnableMvcViewProfiling = true;
      options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
      options.PopupShowTimeWithChildren = true;
      options.PopupShowTrivial = true;
      options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
      options.TrackConnectionOpenClose = true;
    }).AddEntityFramework();
    return builder;
  }
}
