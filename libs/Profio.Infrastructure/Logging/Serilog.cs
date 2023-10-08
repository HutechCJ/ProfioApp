using System.ComponentModel.DataAnnotations;

namespace Profio.Infrastructure.Logging;

public sealed class Serilog
{
  public bool UseConsole { get; set; } = true;

  [Required(ErrorMessage = "Log template is required.")]
  public string LogTemplate { get; set; } =
    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}";

  public string? SeqUrl { get; set; } = "http://localhost:5341";
}
