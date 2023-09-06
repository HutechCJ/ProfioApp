using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Challenge : PageModel
{
  private readonly IIdentityServerInteractionService _interactionService;

  public Challenge(IIdentityServerInteractionService interactionService)
    => _interactionService = interactionService;

  public IActionResult OnGet(string scheme, string returnUrl)
  {
    if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

    if (Url.IsLocalUrl(returnUrl) == false && _interactionService.IsValidReturnUrl(returnUrl) == false)
      throw new("invalid return URL");
    
    var props = new AuthenticationProperties
    {
      RedirectUri = Url.Page("/externallogin/callback"),

      Items =
      {
        { "returnUrl", returnUrl },
        { "scheme", scheme },
      }
    };

    return Challenge(props, scheme);
  }
}
