using Microsoft.Extensions.Configuration;
using Polly;
using Twilio.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace Profio.Infrastructure.Message;

public sealed class TwilioClient : ITwilioRestClient
{
  private readonly ITwilioRestClient _innerClient;
  private readonly ILogger<TwilioClient> _logger;

  public TwilioClient(IConfiguration config, HttpClient httpClient, ILogger<TwilioClient> logger)
  {
    httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "TwilioRestClient");

    _innerClient = new TwilioRestClient(
      config["Twilio:AccountSid"],
      config["Twilio:AuthToken"],
      httpClient: new SystemNetHttpClient(httpClient));

    _logger = logger;
  }

  public Response Request(Request request) => _innerClient.Request(request);

  public async Task<Response> RequestAsync(Request request)
    => await Policy
      .Handle<Exception>()
      .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: _ => TimeSpan.FromMilliseconds(new Random().Next(1000, 3000)),
        onRetry: (_, retryCount, _) =>
          _logger.LogWarning("Failed to send SMS. Retrying... Attempt: {retryCount}", retryCount)
      ).ExecuteAsync(async () =>
        await _innerClient
          .RequestAsync(request)
      );

  public string AccountSid => _innerClient.AccountSid;
  public string Region => _innerClient.Region;
  public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;
}
