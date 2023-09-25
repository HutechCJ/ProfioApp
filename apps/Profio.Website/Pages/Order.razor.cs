using Microsoft.AspNetCore.Components;
using Profio.Website.Services;
using System.Text.RegularExpressions;
using EntityFrameworkCore.Repository.Collections;
using Profio.Website.Cache;
using Profio.Website.Data.Orders;
using Profio.Website.Data.Common;

namespace Profio.Website.Pages;

public partial class Order
{
  private bool Valid { get; set; }
  private bool IsLoading { get; set; }

  private IList<OrderDto> Orders { get; set; } = default!;

  [Parameter]
  public string? PhoneNumber { get; set; }

  [Inject]
  private ICustomerService CustomerService { get; set; } = default!;

  [Inject]
  private ICacheService CacheService { get; set; } = default!;

  [Inject]
  private ILogger<Order> Logger { get; set; } = default!;

  protected override async Task OnInitializedAsync()
  {
    IsLoading = true;

    if (!PhoneRegex().IsMatch(PhoneNumber ?? throw new InvalidOperationException())
        || string.IsNullOrWhiteSpace(PhoneNumber))
    {
      IsLoading = false;
      Valid = false;
      return;
    }

    var currentOrderList = await CacheService.GetOrSetAsync($"order-{PhoneNumber}", () => CustomerService.GetCurrentOrdersByPhoneAsync(PhoneNumber));

    if (currentOrderList?.Data?.Items.Count == 0)
    {
      IsLoading = false;
      Valid = false;
      return;
    }

    Logger.LogInformation("Current order list: {0}", currentOrderList?.Data?.Items);

    Orders = currentOrderList?.Data?.Items ?? throw new InvalidOperationException();

    IsLoading = false;
    Valid = true;
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
