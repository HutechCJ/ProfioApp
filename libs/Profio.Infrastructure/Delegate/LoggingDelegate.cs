using Microsoft.Extensions.Logging;

namespace Profio.Infrastructure.Delegate;

public sealed class LoggingDelegate : DelegatingHandler
{
  private readonly ILogger<LoggingDelegate> _logger;

  public LoggingDelegate(ILogger<LoggingDelegate> logger)
    => _logger = logger;

  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    CancellationToken cancellationToken)
  {
    try
    {
      _logger.LogInformation("Request: {request}", request);
      var response = await base.SendAsync(request, cancellationToken);
      response.EnsureSuccessStatusCode();
      _logger.LogInformation("Response: {response}", response);
      return response;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error: {message}", e.Message);
      throw;
    }
  }
}
