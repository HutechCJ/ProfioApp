using System.ComponentModel.DataAnnotations;

namespace Profio.Infrastructure.Storage.Supabase;

public class Supabase
{
  [Required(ErrorMessage = "Supabase URL is required.")]
  [Url(ErrorMessage = "Supabase URL is invalid.")]
  public string? Url { get; set; }

  [Required(ErrorMessage = "Supabase key is required.")]
  public string? Key { get; set; }
}
