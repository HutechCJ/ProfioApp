using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Profio.Website.Services;
using System.Text.RegularExpressions;
using CurrieTechnologies.Razor.SweetAlert2;
using Profio.Website.Cache;
using Profio.Website.Data.Orders;
using Radzen;

namespace Profio.Website.Pages;

public partial class Order
{
  private bool Valid { get; set; }
  private bool IsLoading { get; set; }
  private int Count { get; set; }
  private int Progress { get; set; }

  private ODataEnumerable<OrderDto>? Orders { get; set; }

  [Parameter]
  public string? PhoneNumber { get; set; }

  [Inject]
  private ICustomerService CustomerService { get; set; } = default!;

  [Inject]
  private ICacheService CacheService { get; set; } = default!;

  [Inject]
  private ILogger<Order> Logger { get; set; } = default!;

  [Inject]
  private SweetAlertService Alert { get; set; } = default!;

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

    Orders = currentOrderList?.Data?.Items.AsODataEnumerable();

    Count = Orders?.Count() ?? 0;

    IsLoading = false;
    Valid = true;
  }

  public async Task OrderDetails(string orderId)
  {
    await Alert.FireAsync("Order Details", JsonSerializer.Serialize(Orders?.FirstOrDefault(order => order.Id == orderId), new JsonSerializerOptions
    {
      WriteIndented = true
    }), SweetAlertIcon.Info);
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
