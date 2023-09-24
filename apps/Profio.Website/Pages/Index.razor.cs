using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using Profio.Website.Services;

namespace Profio.Website.Pages;

public partial class Index
{
  private string PhoneNumber { get; set; } = default!;
  private bool IsLoading { get; set; }

  [Inject]
  private SweetAlertService Alert { get; set; } = default!;
  [Inject]
  private ICustomerService CustomerService { get; set; } = default!;

  public async Task FindAsync()
  {
    IsLoading = true;

    if (string.IsNullOrWhiteSpace(PhoneNumber) || !PhoneRegex().IsMatch(PhoneNumber))
    {
      await Alert.FireAsync("Error", "Invalid phone number!", SweetAlertIcon.Error);
      IsLoading = false;
      return;
    }

    var currentOrderList = await CustomerService.GetCurrentOrdersByPhoneAsync(PhoneNumber);
    var orderList = await CustomerService.GetOrdersByPhoneAsync(PhoneNumber);
    if (currentOrderList == null && orderList == null)
    {
      await Alert.FireAsync("Error", "You don't have any orders!", SweetAlertIcon.Error);
      IsLoading = false;
      return;
    }
    if (currentOrderList?.Data == null)
    {
      await Alert.FireAsync("Error", "Fetching orders failed!", SweetAlertIcon.Error);
      IsLoading = false;
      return;
    }

    await Alert.FireAsync("Oops", string.Join(",", currentOrderList.Data.Items.Select(x => x.Id).ToList()), SweetAlertIcon.Info);
    IsLoading = false;
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
