using IdentityServer.Pages.Consent;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace IdentityServer.Pages.Device;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
  private readonly IDeviceFlowInteractionService _interaction;
  private readonly IEventService _events;
  private readonly IOptions<IdentityServerOptions> _options;
  private readonly ILogger<Index> _logger;

  public Index(
    IDeviceFlowInteractionService interaction,
    IEventService eventService,
    IOptions<IdentityServerOptions> options,
    ILogger<Index> logger)
  {
    _interaction = interaction;
    _events = eventService;
    _options = options;
    _logger = logger;
  }

  public ViewModel View { get; set; }

  [BindProperty]
  public InputModel Input { get; set; }

  public async Task<IActionResult> OnGet(string userCode)
  {
    if (string.IsNullOrWhiteSpace(userCode))
    {
      View = new();
      Input = new();
      return Page();
    }

    View = await BuildViewModelAsync(userCode);
    if (View is null)
    {
      ModelState.AddModelError("", DeviceOptions.InvalidUserCode);
      View = new();
      Input = new();
      return Page();
    }

    Input = new()
    {
      UserCode = userCode,
    };

    return Page();
  }

  public async Task<IActionResult> OnPost()
  {
    var request = await _interaction.GetAuthorizationContextAsync(Input.UserCode);
    if (request == null) return RedirectToPage("/Home/Error/Index");

    ConsentResponse grantedConsent = null;

    switch (Input.Button)
    {
      case "no":
        grantedConsent = new()
        {
          Error = AuthorizationError.AccessDenied
        };

        await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues));
        break;

      case "yes" when Input.ScopesConsented is { } && Input.ScopesConsented.Any():
      {
        var scopes = Input.ScopesConsented;
        if (ConsentOptions.EnableOfflineAccess == false)
        {
          scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
        }

        grantedConsent = new()
        {
          RememberConsent = Input.RememberConsent,
          ScopesValuesConsented = scopes.ToArray(),
          Description = Input.Description
        };

        await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues, grantedConsent.ScopesValuesConsented, grantedConsent.RememberConsent));
        break;
      }
      case "yes":
        ModelState.AddModelError("", ConsentOptions.MustChooseOneErrorMessage);
        break;
      default:
        ModelState.AddModelError("", ConsentOptions.InvalidSelectionErrorMessage);
        break;
    }

    if (grantedConsent is { })
    {
      await _interaction.HandleRequestAsync(Input.UserCode, grantedConsent);
      return RedirectToPage("/Device/Success");
    }

    View = await BuildViewModelAsync(Input.UserCode, Input);
    return Page();
  }


  private async Task<ViewModel> BuildViewModelAsync(string userCode, InputModel model = null)
  {
    var request = await _interaction.GetAuthorizationContextAsync(userCode);
    return request is { }
      ? CreateConsentViewModel(model, request)
      : null;
  }

  private ViewModel CreateConsentViewModel(InputModel model, DeviceFlowAuthorizationRequest request)
  {
    var vm = new ViewModel
    {
      ClientName = request.Client.ClientName ?? request.Client.ClientId,
      ClientUrl = request.Client.ClientUri,
      ClientLogoUrl = request.Client.LogoUri,
      AllowRememberConsent = request.Client.AllowRememberConsent,
      IdentityScopes = request.ValidatedResources.Resources.IdentityResources.Select(x => CreateScopeViewModel(x, model == null || model.ScopesConsented?.Contains(x.Name) == true)).ToArray()
    };

    var apiScopes = (
      from parsedScope in request.ValidatedResources.ParsedScopes
      let apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName)
      where apiScope is { }
      select CreateScopeViewModel(
        parsedScope,
        apiScope,
        model is null || model.ScopesConsented?.Contains(parsedScope.RawValue) == true)).ToList();

    if (DeviceOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
      apiScopes.Add(GetOfflineAccessScope(model == null || model.ScopesConsented?.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) == true));

    vm.ApiScopes = apiScopes;

    return vm;
  }

  public ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
  {
    return new()
    {
      Value = identity.Name,
      DisplayName = identity.DisplayName ?? identity.Name,
      Description = identity.Description,
      Emphasize = identity.Emphasize,
      Required = identity.Required,
      Checked = check || identity.Required
    };
  }

  public ScopeViewModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
  {
    return new()
    {
      Value = parsedScopeValue.RawValue,
      DisplayName = apiScope.DisplayName ?? apiScope.Name,
      Description = apiScope.Description,
      Emphasize = apiScope.Emphasize,
      Required = apiScope.Required,
      Checked = check || apiScope.Required
    };
  }

  public ScopeViewModel GetOfflineAccessScope(bool check)
  {
    return new()
    {
      Value = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
      DisplayName = DeviceOptions.OfflineAccessDisplayName,
      Description = DeviceOptions.OfflineAccessDescription,
      Emphasize = true,
      Checked = check
    };
  }
}
