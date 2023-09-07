using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;

namespace IdentityServer.Pages.Grants;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
  private readonly IIdentityServerInteractionService _interaction;
  private readonly IClientStore _clients;
  private readonly IResourceStore _resources;
  private readonly IEventService _events;

  public Index(IIdentityServerInteractionService interaction,
    IClientStore clients,
    IResourceStore resources,
    IEventService events)
  {
    _interaction = interaction;
    _clients = clients;
    _resources = resources;
    _events = events;
  }

  public ViewModel View { get; set; }

  public async Task OnGet()
  {
    var grants = await _interaction.GetAllUserGrantsAsync();

    var list = new List<GrantViewModel>();
    foreach (var grant in grants)
    {
      var client = await _clients.FindClientByIdAsync(grant.ClientId);
      if (client is null)
        continue;

      var resources = await _resources.FindResourcesByScopeAsync(grant.Scopes);

      var item = new GrantViewModel()
      {
        ClientId = client.ClientId,
        ClientName = client.ClientName ?? client.ClientId,
        ClientLogoUrl = client.LogoUri,
        ClientUrl = client.ClientUri,
        Description = grant.Description,
        Created = grant.CreationTime,
        Expires = grant.Expiration,
        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
        ApiGrantNames = resources.ApiScopes.Select(x => x.DisplayName ?? x.Name).ToArray()
      };

      list.Add(item);
    }

    View = new()
    {
      Grants = list
    };
  }

  [BindProperty]
  [Required]
  public string ClientId { get; set; }

  public async Task<IActionResult> OnPost()
  {
    await _interaction.RevokeUserConsentAsync(ClientId);
    await _events.RaiseAsync(new GrantsRevokedEvent(User.GetSubjectId(), ClientId));

    return RedirectToPage("/Grants/Index");
  }
}
