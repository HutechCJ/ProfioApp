using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using Ganss.Xss;

namespace Profio.Infrastructure.Middleware;

public class XssProtectionMiddleware
{
  private readonly RequestDelegate _next;

  public XssProtectionMiddleware(RequestDelegate next)
    => _next = next;

  public async Task InvokeAsync(HttpContext context)
  {
    context.Request.EnableBuffering();
    using var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
    var body = await streamReader.ReadToEndAsync();
    var sanitizedBody = new HtmlSanitizer().Sanitize(body);

    if (body != sanitizedBody)
    {
      context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(sanitizedBody));
      context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }

    context.Request.Body.Seek(0, SeekOrigin.Begin);

    await _next.Invoke(context);
  }

}
