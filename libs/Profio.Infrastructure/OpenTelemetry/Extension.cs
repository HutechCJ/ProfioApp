using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Profio.Infrastructure.OpenTelemetry;

public static class Extension
{
  public static void AddOpenTelemetry(this WebApplicationBuilder builder)
  {
    var resourceBuilder = ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName);

    builder.Services.AddOpenTelemetry()
      .WithTracing(trace =>
        trace.SetResourceBuilder(resourceBuilder)
          .AddOtlpExporter()
          .AddSource("Microsoft.AspNetCore", "System.Net.Http")
          .AddEntityFrameworkCoreInstrumentation()
      )
      .WithMetrics(meter =>
        meter.SetResourceBuilder(resourceBuilder)
          .AddPrometheusExporter()
          .AddOtlpExporter()
          .AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel", "System.Net.Http")
          .AddView("http.server.request.duration",
            new ExplicitBucketHistogramConfiguration
            {
              Boundaries = new[] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
            })
      );
  }
}
