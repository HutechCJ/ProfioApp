using IdentityServer4.Models;

namespace IdentityServer.Pages.Home.Error;

public class ViewModel
{
  public ViewModel()
  {
  }

  public ViewModel(string error)
  {
    Error = new ErrorMessage { Error = error };
  }

  public ErrorMessage Error { get; set; }
}
