using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profio.Infrastructure.Identity;

namespace IdentityServer.Pages.Account.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly IIdentityServerInteractionService _interaction;
  private readonly IEventService _events;

  [BindProperty]
  public string LogoutId { get; set; } = default!;

  public Index(
    SignInManager<ApplicationUser> signInManager,
    IIdentityServerInteractionService interaction,
    IEventService events)
  {
    _signInManager = signInManager;
    _interaction = interaction;
    _events = events;
  }

  public async Task<IActionResult> OnGet(string logoutId)
  {
    LogoutId = logoutId;

    var showLogoutPrompt = LogoutOptions.ShowLogoutPrompt;

    if (User.Identity?.IsAuthenticated != true)
      showLogoutPrompt = false;
    
    else
    {
      var context = await _interaction.GetLogoutContextAsync(LogoutId);
      if (context.ShowSignoutPrompt == false)
        showLogoutPrompt = false;
    }

    if (showLogoutPrompt == false)
      return await OnPost();

    return Page();
  }

  public async Task<IActionResult> OnPost()
  {
    if (User.Identity?.IsAuthenticated != true)
      return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });

    LogoutId ??= await _interaction.CreateLogoutContextAsync();
    await _signInManager.SignOutAsync();
    await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));

    var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

    if (idp is null or IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
      return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });

    if (!await HttpContext.GetSchemeSupportsSignOutAsync(idp))
      return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });

    var url = Url.Page("/Account/Logout/Loggedout", new { logoutId = LogoutId });

    return SignOut(new AuthenticationProperties { RedirectUri = url }, idp);

  }
}
