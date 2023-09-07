using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages;

[AllowAnonymous]
public class Index : PageModel
{
  public string Version;

  public void OnGet()
  {
    Version = typeof(IdentityServer4.Hosting.IdentityServerMiddleware).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+').First();
  }
}
