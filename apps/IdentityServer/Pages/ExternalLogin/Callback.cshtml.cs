using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Profio.Infrastructure.Identity;

namespace IdentityServer.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Callback : PageModel
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly IIdentityServerInteractionService _interaction;
  private readonly ILogger<Callback> _logger;
  private readonly IEventService _events;

  public Callback(
    IIdentityServerInteractionService interaction,
    IEventService events,
    ILogger<Callback> logger,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _interaction = interaction;
    _logger = logger;
    _events = events;
  }

  public async Task<IActionResult> OnGet()
  {
    var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
    if (result.Succeeded != true)
      throw new("External authentication error");

    var externalUser = result.Principal;

    if (_logger.IsEnabled(LogLevel.Debug))
    {
      var externalClaims = externalUser.Claims.Select(c => $"{c.Type}: {c.Value}");
      _logger.LogDebug("External claims: {@claims}", externalClaims);
    }

    var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                      externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                      throw new("Unknown userid");

    var provider = result.Properties.Items["scheme"];
    var providerUserId = userIdClaim.Value;
    var user = await _userManager.FindByLoginAsync(provider, providerUserId) ?? await AutoProvisionUserAsync(provider, providerUserId, externalUser.Claims);
    var additionalLocalClaims = new List<Claim>();
    var localSignInProps = new AuthenticationProperties();
    CaptureExternalLoginContext(result, additionalLocalClaims, localSignInProps);


    await _signInManager.SignInWithClaimsAsync(user, localSignInProps, additionalLocalClaims);
    await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

    var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";
    var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
    await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.Id, user.UserName, true, context?.Client.ClientId));

    if (context is null)
      return Redirect(returnUrl);

    return context.IsNativeClient()
      ? this.LoadingPage(returnUrl)
      : Redirect(returnUrl);
  }

  private async Task<ApplicationUser> AutoProvisionUserAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
  {
    var sub = Guid.NewGuid().ToString();

    var user = new ApplicationUser
    {
      Id = sub,
      UserName = sub
    };

    var enumerable = claims.ToList();
    var email = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
                enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
    if (email is { })
      user.Email = email;

    var filtered = new List<Claim>();

    var name = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ??
               enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
    if (name is { })
      filtered.Add(new(JwtClaimTypes.Name, name));
    
    else
    {
      var first = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value ??
                  enumerable.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
      var last = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value ??
                 enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
      switch (first)
      {
        case { } when last is { }:
          filtered.Add(new(JwtClaimTypes.Name, first + " " + last));
          break;

        case { }:
          filtered.Add(new(JwtClaimTypes.Name, first));
          break;

        default:
        {
          if (last is { })
            filtered.Add(new(JwtClaimTypes.Name, last));

          break;
        }
      }
    }

    var identityResult = await _userManager.CreateAsync(user);
    if (!identityResult.Succeeded) throw new(identityResult.Errors.First().Description);

    if (filtered.Any())
    {
      identityResult = await _userManager.AddClaimsAsync(user, filtered);
      if (!identityResult.Succeeded) throw new(identityResult.Errors.First().Description);
    }

    identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));
    if (!identityResult.Succeeded) throw new(identityResult.Errors.First().Description);

    return user;
  }

  public void CaptureExternalLoginContext(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
  {
    localClaims.Add(new(JwtClaimTypes.IdentityProvider, externalResult.Properties!.Items["scheme"]));
    var sid = externalResult.Principal?.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
    if (sid is { })
      localClaims.Add(new(JwtClaimTypes.SessionId, sid.Value));

    var idToken = externalResult.Properties.GetTokenValue("id_token");
    if (idToken is { })
      localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
  }
}
