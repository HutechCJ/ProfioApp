using System.Net;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace Profio.Infrastructure.Middleware;

public class XsrfProtectionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly IAntiforgery _antiforgery;

  public XsrfProtectionMiddleware(RequestDelegate next, IAntiforgery antiforgery)
    => (_next, _antiforgery) = (next, antiforgery);

  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Method.Equals("GET") || context.Request.Method.Equals("OPTIONS"))
    {
      await _next(context);
      return;
    }

    try
    {
      await _antiforgery.ValidateRequestAsync(context);
      await _next(context);
    }
    catch (AntiforgeryValidationException)
    {
      context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
      await context.Response.WriteAsync("XSRF Token Validation Failed.");
    }
  }
}
