using Microsoft.AspNetCore.Components;
using ILogger = Spark.Library.Logging.ILogger;

namespace Profio.Web.Pages;

public partial class Index
{
  [Inject]
  public ILogger Logger { get; set; } = default!;

  protected override void OnInitialized()
  {
    Logger.Information($"Initialized at {DateTime.Now}");
  }
}
