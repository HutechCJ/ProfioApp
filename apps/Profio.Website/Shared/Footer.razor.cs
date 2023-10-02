using System.ComponentModel.DataAnnotations;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;

namespace Profio.Website.Shared;

public partial class Footer
{
  public string Email { get; set; } = default!;

  [Inject] private SweetAlertService Alert { get; set; } = default!;

  public async Task SaveEmailAsync()
  {
    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emails.csv");

    var emails = await File.ReadAllLinesAsync(path);

    if (string.IsNullOrWhiteSpace(Email) || !new EmailAddressAttribute().IsValid(Email))
    {
      await Alert.FireAsync("Error", "Invalid email!", SweetAlertIcon.Error);
      return;
    }

    if (emails.Contains(Email))
    {
      await Alert.FireAsync("Error", "Email already exists!", SweetAlertIcon.Error);
      Email = string.Empty;
      return;
    }

    await File.AppendAllTextAsync(path, $"{Email}\n");

    await Alert.FireAsync("Success", "Thank you for subscribing!", SweetAlertIcon.Success);

    Email = string.Empty;
  }
}
