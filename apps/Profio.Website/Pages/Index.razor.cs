using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using Profio.Website.Cache;
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

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private ICacheService CacheService { get; set; } = default!;

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

    switch (currentOrderList?.Data?.Items.Count)
    {
      case 0 when orderList?.Data?.Items.Count == 0:
        await Alert.FireAsync("Error", "You don't have any orders!", SweetAlertIcon.Error);
        IsLoading = false;
        return;
      case > 0:
        await CacheService.GetOrSetAsync($"order-{PhoneNumber}", () => CustomerService.GetCurrentOrdersByPhoneAsync(PhoneNumber));
        await CacheService.GetOrSetAsync($"history-{PhoneNumber}", () => CustomerService.GetOrdersByPhoneAsync(PhoneNumber));
        NavigationManager.NavigateTo($"/order/{PhoneNumber}");
        IsLoading = false;
        return;
      default:
        await CacheService.GetOrSetAsync($"history-{PhoneNumber}", () => CustomerService.GetOrdersByPhoneAsync(PhoneNumber));
        NavigationManager.NavigateTo($"/history/{PhoneNumber}");
        IsLoading = false;
        break;
    }
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
