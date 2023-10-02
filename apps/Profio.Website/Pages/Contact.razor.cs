using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Profio.Website.Pages;

public partial class Contact
{
  [Required(ErrorMessage = "Name is required.")]
  private string? Name { get; set; }

  [Required(ErrorMessage = "Email is required.")]
  [EmailAddress(ErrorMessage = "Invalid email format.")]
  private string? Email { get; set; }

  [Required(ErrorMessage = "Message is required.")]
  private string? Message { get; set; }

  [MaxLength(100, ErrorMessage = "Subject must be less than 100 characters.")]
  private string? Subject { get; set; }

  [Inject] private NavigationManager NavigationManager { get; set; } = default!;

  [Inject] private IConfiguration Configuration { get; set; } = default!;

  public void SendMessage()
  {
    var message = $"Hi, I'm {Name} ({Email}). {Message}";

    var encodedSubject = Uri.EscapeDataString(Subject ?? "Contact Us");
    var encodedMessage = Uri.EscapeDataString(message);

    var mailtoUrl = $"mailto:{Configuration["ContactEmail"]}?subject={encodedSubject}&body={encodedMessage}";

    NavigationManager.NavigateTo(mailtoUrl, true);
  }
}
