using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Profio.Website.Shared;

public partial class NavMenu
{
  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;
  private static string _currentPath = string.Empty;

  private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
  {
    _currentPath = NavigationManager.Uri[NavigationManager.BaseUri.Length..];
    StateHasChanged();
  }

  private static string IsActive(string path)
    => _currentPath == path ? "active" : string.Empty;

  protected override void OnInitialized()
  {
    NavigationManager.LocationChanged += HandleLocationChanged;
    base.OnInitialized();
  }

  public void Dispose()
  {
    NavigationManager.LocationChanged -= HandleLocationChanged;
    GC.SuppressFinalize(this);
  }
}
