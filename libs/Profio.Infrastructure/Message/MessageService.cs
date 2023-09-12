using System.Net;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json.Linq;

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

  public string SendSms(string[] phones, string content, string sender)
  {
    if (phones.Length <= 0)
      return "";
    if (content.Equals(""))
      return "";

    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("Authorization", _accessToken);
    httpClient.DefaultRequestHeaders.Accept.Add(new(MediaTypeNames.Application.Json));
    var json = new JObject
    {
      ["to"] = JToken.FromObject(phones),
      ["content"] = EncodeNonAsciiCharacters(content),
      ["sms_type"] = "2",
      ["sender"] = sender
    };

    var response = httpClient.PostAsync($"{RootUrl}/sms/send", new StringContent(json.ToString(), Encoding.UTF8, MediaTypeNames.Application.Json)).Result;
    if (response.StatusCode != HttpStatusCode.OK) return "";
    var result = response.Content.ReadAsStringAsync().Result;
    return result;
  }

  public string SendMms(string[] phones, string content, string link, string sender)
  {
    if (phones.Length <= 0)
      return "";
    if (content.Equals(""))
      return "";

    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("Authorization", _accessToken);
    httpClient.DefaultRequestHeaders.Accept.Add(new(MediaTypeNames.Application.Json));
    var json = new JObject
    {
      ["to"] = JToken.FromObject(phones),
      ["content"] = EncodeNonAsciiCharacters(content),
      ["sms_type"] = "2",
      ["sender"] = sender,
      ["link"] = link
    };

    var response = httpClient.PostAsync($"{RootUrl}/mms/send", new StringContent(json.ToString(), Encoding.UTF8, MediaTypeNames.Application.Json)).Result;
    if (response.StatusCode != HttpStatusCode.OK) return "";
    var result = response.Content.ReadAsStringAsync().Result;
    return result;
  }

  private static string EncodeNonAsciiCharacters(string value)
  {
    var sb = new StringBuilder();
    foreach (var c in value)
    {
      if (c > 127)
      {
        var encodedValue = "\\u" + ((int)c).ToString("x4");
        sb.Append(encodedValue);
      }
      else
      {
        sb.Append(c);
      }
    }
    return sb.ToString();
  }
}
