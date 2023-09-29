using Microsoft.Extensions.Configuration;

namespace Profio.Infrastructure.Delegate;

public class AuthenticationDelegate : DelegatingHandler
{
  private readonly IConfiguration _configuration;

  public AuthenticationDelegate(IConfiguration configuration)
    => _configuration = configuration;
  
  protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
  {
    request.Headers.Add("X-API-Key", _configuration["ApiKey"]);
    return await base.SendAsync(request, cancellationToken);
  }
}
