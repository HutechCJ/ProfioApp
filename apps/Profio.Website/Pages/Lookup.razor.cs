using Microsoft.AspNetCore.Components;
using Profio.Website.Services;
using System.Text.RegularExpressions;
using Profio.Website.Cache;
using Profio.Website.Data.Orders;
using Radzen;
using Radzen.Blazor;
using AngleSharp.Browser.Dom;

namespace Profio.Website.Pages;

public partial class Lookup
{
  private bool Valid { get; set; }
  private bool IsLoading { get; set; }
  private int CountOrders { get; set; }
  private int CountHistory { get; set; }

  private ODataEnumerable<OrderDto>? Orders { get; set; }
  private RadzenDataGrid<OrderDto> Grid { get; set; } = default!;
  private IEnumerable<OrderDto>? History { get; set; } = default!;
  private IList<OrderDto> SelectedOrders { get; set; } = default!;

  [Parameter]
  public string? PhoneNumber { get; set; }

  [Inject]
  private ICustomerService CustomerService { get; set; } = default!;

  [Inject]
  private ICacheService CacheService { get; set; } = default!;

  [Inject]
  private ILogger<Lookup> Logger { get; set; } = default!;

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

    var orderList = await CacheService.GetOrSetAsync($"history-{PhoneNumber}", () => CustomerService.GetOrdersByPhoneAsync(PhoneNumber));

    if (orderList?.Data?.Items.Count == 0)
    {
      IsLoading = false;
      Valid = false;
      return;
    }

    Logger.LogInformation("Current order list: {0}", currentOrderList?.Data?.Items);

    Orders = currentOrderList?.Data?.Items.AsODataEnumerable();

    History = orderList?.Data?.Items.AsODataEnumerable();

    CountOrders = Orders?.Count() ?? 0;

    CountHistory = History?.Count() ?? 0;

    IsLoading = false;
    Valid = true;
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
