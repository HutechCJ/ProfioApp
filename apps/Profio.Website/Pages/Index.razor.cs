using System.Text.RegularExpressions;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;

namespace Profio.Website.Pages;

public partial class Index
{
  private string PhoneNumber { get; set; } = default!;

  [Inject]
  private SweetAlertService Alert { get; set; } = default!;

  public async Task FindAsync()
  {
    if (string.IsNullOrWhiteSpace(PhoneNumber) || !PhoneRegex().IsMatch(PhoneNumber))
    {
      await Alert.FireAsync("Error", "Invalid phone number!", SweetAlertIcon.Error);
      return;
    }

    await Alert.FireAsync("Oops", "You don't have any order yet!", SweetAlertIcon.Info);
  }

  [GeneratedRegex("^\\d{10}$")]
  private static partial Regex PhoneRegex();
}
