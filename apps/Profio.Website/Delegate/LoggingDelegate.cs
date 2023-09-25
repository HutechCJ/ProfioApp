namespace Profio.Website.Delegate;

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
      _logger.LogInformation("Request: {0}", request);
      var response = await base.SendAsync(request, cancellationToken);
      response.EnsureSuccessStatusCode();
      _logger.LogInformation("Response: {0}", response);
      return response;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error: {0}", e.Message);
      throw;
    }
  }
}
