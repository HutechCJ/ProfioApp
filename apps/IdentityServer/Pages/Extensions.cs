using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages;

public static class Extensions
{
  public static async Task<bool> GetSchemeSupportsSignOutAsync(this HttpContext context, string scheme)
  {
    var provider = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
    var handler = await provider.GetHandlerAsync(context, scheme);
    return (handler is IAuthenticationSignOutHandler);
  }

  public static bool IsNativeClient(this AuthorizationRequest context)
  {
    return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
           && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
  }

  public static IActionResult LoadingPage(this PageModel page, string redirectUri)
  {
    page.HttpContext.Response.StatusCode = 200;
    page.HttpContext.Response.Headers["Location"] = "";
    return page.RedirectToPage("/Redirect/Index", new { RedirectUri = redirectUri });
  }
}
