using System.Net;
using System.Text.RegularExpressions;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;

namespace Profio.Website.Pages;

public partial class Index
{
  private string PhoneNumber { get; set; } = default!;
  private bool IsLoading { get; set; }

  [Inject]
  private IConfiguration Configuration { get; set; } = default!;

  [Inject]
  private SweetAlertService Alert { get; set; } = default!;

  [Inject]
  private IHttpClientFactory ClientFactory { get; set; } = default!;

  public async Task FindAsync()
  {
    IsLoading = true;

    if (string.IsNullOrWhiteSpace(PhoneNumber) || !PhoneRegex().IsMatch(PhoneNumber))
    {
      await Alert.FireAsync("Error", "Invalid phone number!", SweetAlertIcon.Error);
      IsLoading = false;
      return;
    }

    var order = new HttpRequestMessage(HttpMethod.Get, Configuration["ApiUrl"] + $"/customers/{PhoneNumber}/orders/current");

    var history = new HttpRequestMessage(HttpMethod.Get, Configuration["ApiUrl"] + $"/customers/{PhoneNumber}/orders");

    var client = ClientFactory.CreateClient();

    var senderOrder = await client.SendAsync(order);
    var senderHistory = await client.SendAsync(history);

    if (senderOrder.StatusCode == HttpStatusCode.NoContent && senderHistory.StatusCode == HttpStatusCode.NoContent)
    {
      await Alert.FireAsync("Error", "You don't have any orders!", SweetAlertIcon.Error);
      return;
    }

    await Alert.FireAsync("Oops", "ABC", SweetAlertIcon.Info);
    IsLoading = false;
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
