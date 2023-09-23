using Microsoft.Extensions.Configuration;
using Twilio.Clients;
using Twilio.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace Profio.Infrastructure.Message;

public sealed class TwilioClient : ITwilioRestClient
{
  private readonly ITwilioRestClient _innerClient;

  public TwilioClient(IConfiguration config, HttpClient httpClient)
  {
    httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "TwilioRestClient");

    _innerClient = new TwilioRestClient(
      config["Twilio:AccountSid"],
      config["Twilio:AuthToken"],
      httpClient: new SystemNetHttpClient(httpClient));
  }

  public Response Request(Request request) => _innerClient.Request(request);
  public async Task<Response> RequestAsync(Request request) => await _innerClient.RequestAsync(request);
  public string AccountSid => _innerClient.AccountSid;
  public string Region => _innerClient.Region;
  public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;
}
