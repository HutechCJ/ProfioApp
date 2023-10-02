using Microsoft.AspNetCore.Components;

namespace Profio.Website.Pages;

public partial class Service
{
  private string? Name { get; set; }
  private string? Email { get; set; }
  private string? OptionService { get; set; }

  [Inject] private NavigationManager NavigationManager { get; set; } = default!;

  [Inject] private IConfiguration Configuration { get; set; } = default!;

  public void SendMessage()
  {
    var message = $"Hi, I'm {Name} ({Email}). I want to get a quote for {OptionService}.";

    var encodedMessage = Uri.EscapeDataString(message);
    var encodedSubject = Uri.EscapeDataString("Get a Quote");

    var mailtoUrl = $"mailto:{Configuration["ContactEmail"]}?subject={encodedSubject}&body={encodedMessage}";

    NavigationManager.NavigateTo(mailtoUrl, true);
  }
}
