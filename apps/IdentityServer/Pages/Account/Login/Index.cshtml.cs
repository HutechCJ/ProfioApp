using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profio.Infrastructure.Identity;

namespace IdentityServer.Pages.Account.Login;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly IIdentityServerInteractionService _interaction;
  private readonly IEventService _events;
  private readonly IAuthenticationSchemeProvider _schemeProvider;
  private readonly IIdentityProviderStore _identityProviderStore;

  public ViewModel View { get; set; } = default!;

  [BindProperty]
  public InputModel Input { get; set; } = default!;

  public Index(
    IIdentityServerInteractionService interaction,
    IAuthenticationSchemeProvider schemeProvider,
    IIdentityProviderStore identityProviderStore,
    IEventService events,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _interaction = interaction;
    _schemeProvider = schemeProvider;
    _identityProviderStore = identityProviderStore;
    _events = events;
  }

  public async Task<IActionResult> OnGet(string returnUrl)
  {
    await BuildModelAsync(returnUrl);

    if (View.IsExternalLoginOnly)
      return RedirectToPage("/ExternalLogin/Challenge", new { scheme = View.ExternalLoginScheme, returnUrl });

    return Page();
  }

  public async Task<IActionResult> OnPost()
  {
    var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

    if (Input.Button != "login")
    {
      if (context is null)
        return Redirect("~/");

      await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

      return context.IsNativeClient()
        ? this.LoadingPage(Input.ReturnUrl)
        : Redirect(Input.ReturnUrl);
    }

    if (ModelState.IsValid)
    {
      var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberLogin, lockoutOnFailure: true);
      if (result.Succeeded)
      {
        var user = await _userManager.FindByNameAsync(Input.Username);
        await _events.RaiseAsync(new UserLoginSuccessEvent(user?.UserName, user?.Id, user?.UserName, clientId: context?.Client.ClientId));

        if (context is { })
        {
          return context.IsNativeClient()
            ? this.LoadingPage(Input.ReturnUrl)
            : Redirect(Input.ReturnUrl);
        }

        if (Url.IsLocalUrl(Input.ReturnUrl))
          return Redirect(Input.ReturnUrl);

        if (string.IsNullOrEmpty(Input.ReturnUrl))
          return Redirect("~/");

        throw new("invalid return URL");
      }

      await _events.RaiseAsync(new UserLoginFailureEvent(Input.Username, "invalid credentials", clientId: context?.Client.ClientId));
      ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
    }

    await BuildModelAsync(Input.ReturnUrl);
    return Page();
  }

  private async Task BuildModelAsync(string returnUrl)
  {
    Input = new()
    {
      ReturnUrl = returnUrl
    };

    var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
    if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
    {
      var local = context.IdP == Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider;
      View = new()
      {
        EnableLocalLogin = local,
      };

      Input.Username = context.LoginHint;

      if (!local)
      {
        View.ExternalProviders = new[] { new ViewModel.ExternalProvider { AuthenticationScheme = context.IdP } };
      }

      return;
    }

    var schemes = await _schemeProvider.GetAllSchemesAsync();

    var providers = schemes
      .Where(x => x.DisplayName != null)
      .Select(x => new ViewModel.ExternalProvider
      {
        DisplayName = x.DisplayName ?? x.Name,
        AuthenticationScheme = x.Name
      }).ToList();

    var dynamicSchemes = (await _identityProviderStore.GetAllSchemeNamesAsync())
      .Where(x => x.Enabled)
      .Select(x => new ViewModel.ExternalProvider
      {
        AuthenticationScheme = x.Scheme,
        DisplayName = x.DisplayName
      });
    providers.AddRange(dynamicSchemes);


    var allowLocal = true;
    var client = context?.Client;
    if (client is { })
    {
      allowLocal = client.EnableLocalLogin;
      if (client.IdentityProviderRestrictions.Any())
      {
        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
      }
    }

    View = new()
    {
      AllowRememberLogin = LoginOptions.AllowRememberLogin,
      EnableLocalLogin = allowLocal && LoginOptions.AllowLocalLogin,
      ExternalProviders = providers.ToArray()
    };
  }
}
