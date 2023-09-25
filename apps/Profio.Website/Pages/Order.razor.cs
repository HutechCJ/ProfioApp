using Microsoft.AspNetCore.Components;
using Profio.Website.Services;
using System.Text.RegularExpressions;

namespace Profio.Website.Pages;

public partial class Order
{
  private bool Valid { get; set; }
  private bool IsLoading { get; set; }

  [Parameter]
  public string? PhoneNumber { get; set; }

  [Inject]
  private ICustomerService CustomerService { get; set; } = default!;

  protected override async Task OnInitializedAsync()
  {
    IsLoading = true;

    if (!PhoneRegex().IsMatch(PhoneNumber ?? throw new InvalidOperationException()) || string.IsNullOrWhiteSpace(PhoneNumber))
    {
      IsLoading = false;
      Valid = false;
      return;
    }

    var currentOrderList = await CustomerService.GetCurrentOrdersByPhoneAsync(PhoneNumber);

    if (currentOrderList?.Data?.Items.Count == 0)
    {
      IsLoading = false;
      Valid = false;
      return;
    }

    Valid = true;
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
