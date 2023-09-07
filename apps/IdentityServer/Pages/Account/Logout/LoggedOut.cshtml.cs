using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Account.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class LoggedOut : PageModel
{
  private readonly IIdentityServerInteractionService _interactionService;

  public LoggedOutViewModel View { get; set; } = default!;

  public LoggedOut(IIdentityServerInteractionService interactionService)
    => _interactionService = interactionService;

  public async Task OnGet(string logoutId)
  {
    var logout = await _interactionService.GetLogoutContextAsync(logoutId);

    View = new()
    {
      AutomaticRedirectAfterSignOut = LogoutOptions.AutomaticRedirectAfterSignOut,
      PostLogoutRedirectUri = logout?.PostLogoutRedirectUri!,
      ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId! : logout.ClientName!,
      SignOutIframeUrl = logout?.SignOutIFrameUrl!
    };
  }
}
