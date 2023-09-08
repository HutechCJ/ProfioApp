using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Consent;

[Authorize]
[SecurityHeaders]
public class Index : PageModel
{
  private readonly IIdentityServerInteractionService _interaction;
  private readonly IEventService _events;
  private readonly ILogger<Index> _logger;

  public Index(
    IIdentityServerInteractionService interaction,
    IEventService events,
    ILogger<Index> logger)
  {
    _interaction = interaction;
    _events = events;
    _logger = logger;
  }

  public ViewModel View { get; set; }

  [BindProperty]
  public InputModel Input { get; set; }

  public async Task<IActionResult> OnGet(string returnUrl)
  {
    View = await BuildViewModelAsync(returnUrl);
    if (View == null)
    {
      return RedirectToPage("/Home/Error/Index");
    }

    Input = new()
    {
      ReturnUrl = returnUrl,
    };

    return Page();
  }

  public async Task<IActionResult> OnPost()
  {
    var request = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);
    if (request == null) return RedirectToPage("/Home/Error/Index");

    ConsentResponse grantedConsent = null;

    switch (Input?.Button)
    {
      case "no":
        grantedConsent = new() { Error = AuthorizationError.AccessDenied };
        await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues));
        break;

      case "yes" when Input.ScopesConsented != null && Input.ScopesConsented.Any():
      {
        var scopes = Input.ScopesConsented;
        if (ConsentOptions.EnableOfflineAccess == false)
        {
          scopes = scopes.Where(x => x != Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess);
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
      await _interaction.GrantConsentAsync(request, grantedConsent);

      return request.IsNativeClient()
        ? this.LoadingPage(Input.ReturnUrl)
        : Redirect(Input.ReturnUrl);
    }

    View = await BuildViewModelAsync(Input?.ReturnUrl, Input);
    return Page();
  }

  private async Task<ViewModel> BuildViewModelAsync(string returnUrl, InputModel model = null)
  {
    var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
    if (request is { })
    {
      return CreateConsentViewModel(model, returnUrl, request);
    }

    _logger.LogError("No consent request matching request: {0}", returnUrl);
    return null;
  }

  private ViewModel CreateConsentViewModel(
    InputModel model, string returnUrl,
    AuthorizationRequest request)
  {
    var vm = new ViewModel
    {
      ClientName = request.Client.ClientName ?? request.Client.ClientId,
      ClientUrl = request.Client.ClientUri,
      ClientLogoUrl = request.Client.LogoUri,
      AllowRememberConsent = request.Client.AllowRememberConsent,
      IdentityScopes = request.ValidatedResources.Resources.IdentityResources
        .Select(x => CreateScopeViewModel(x, model?.ScopesConsented == null || model.ScopesConsented?.Contains(x.Name) == true))
        .ToArray()
    };

    var resourceIndicators = request.Parameters.GetValues(OidcConstants.AuthorizeRequest.Resource) ?? Enumerable.Empty<string>();
    var apiResources = request.ValidatedResources.Resources.ApiResources.Where(x => resourceIndicators.Contains(x.Name)).ToList();

    var apiScopes = new List<ScopeViewModel>();
    foreach (var parsedScope in request.ValidatedResources.ParsedScopes)
    {
      var apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
      if (apiScope is null)
        continue;

      var scopeVm = CreateScopeViewModel(parsedScope, apiScope, model == null || model.ScopesConsented?.Contains(parsedScope.RawValue) == true);
      scopeVm.Resources = apiResources.Where(x => x.Scopes.Contains(parsedScope.ParsedName))
        .Select(x => new ResourceViewModel
        {
          Name = x.Name,
          DisplayName = x.DisplayName ?? x.Name,
        }).ToArray();
      apiScopes.Add(scopeVm);
    }
    if (ConsentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
    {
      apiScopes.Add(GetOfflineAccessScope(model == null || model.ScopesConsented?.Contains(Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess) == true));
    }
    vm.ApiScopes = apiScopes;

    return vm;
  }

  public ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
  {
    return new()
    {
      Name = identity.Name,
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
    var displayName = apiScope.DisplayName ?? apiScope.Name;
    if (!string.IsNullOrWhiteSpace(parsedScopeValue.ParsedParameter))
      displayName += ":" + parsedScopeValue.ParsedParameter;

    return new()
    {
      Name = parsedScopeValue.ParsedName,
      Value = parsedScopeValue.RawValue,
      DisplayName = displayName,
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
      Value = Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess,
      DisplayName = ConsentOptions.OfflineAccessDisplayName,
      Description = ConsentOptions.OfflineAccessDescription,
      Emphasize = true,
      Checked = check
    };
  }
}