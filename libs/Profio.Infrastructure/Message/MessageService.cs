using System.Net;
using System.Text;
using Newtonsoft.Json;
using Profio.Infrastructure.Exceptions;

namespace Profio.Infrastructure.Message;

public sealed class MessageService : IMessageService
{
  private const string RootUrl = "https://api.speedsms.vn/index.php";
  private readonly string? _accessToken;

  public MessageService(string? accessToken)
    => _accessToken = accessToken;

  public string? GetUserInfo()
  {
    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("Authorization", _accessToken);
    var response = httpClient.GetAsync($"{RootUrl}/user/info").Result;
    if (response.StatusCode != HttpStatusCode.OK) return null;
    var result = response.Content.ReadAsStringAsync().Result;
    return result;
  }

  public async Task<string> SendSms(string phones, string content)
  {
    if (string.IsNullOrEmpty(phones) || string.IsNullOrEmpty(content))
      return "";

    var client = new HttpClient();
    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_accessToken}:x"));
    client.DefaultRequestHeaders.Authorization = new("Basic", credentials);
    client.DefaultRequestHeaders.Accept.Add(new("application/json"));

    const string sender = "";
    var phoneNumbers = phones.Split(',');

    var smsData = new
    {
      to = phoneNumbers,
      content,
      type = 2,
      sender
    };

    var json = JsonConvert.SerializeObject(smsData);

    var contentToSend = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await client.PostAsync($"{RootUrl}/sms/send", contentToSend);

    if (!response.IsSuccessStatusCode)
      throw new SmsException(response.StatusCode, response.RequestMessage!.ToString());

    var responseContent = await response.Content.ReadAsStringAsync();
    return responseContent;

  }
}
