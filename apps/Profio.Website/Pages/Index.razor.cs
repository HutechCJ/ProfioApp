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
      await Alert.FireAsync("Error", "Invalid phone number!", SweetAlertIcon.Info);
      IsLoading = false;
      return;
    }

    var currentOrderList = await CustomerService.GetCurrentOrdersByPhoneAsync(PhoneNumber);
    var orderList = await CustomerService.GetOrdersByPhoneAsync(PhoneNumber);

    if (currentOrderList?.Data?.Items.Count == 0 && orderList?.Data?.Items.Count == 0)
    {
      await Alert.FireAsync("Error", "You don't have any orders!", SweetAlertIcon.Error);
      IsLoading = false;
      return;
    }

    IsLoading = false;
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
