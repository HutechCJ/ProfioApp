using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Profio.Infrastructure.Identity;

namespace Profio.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
  private readonly UserManager<ApplicationUser> _userManager;

  public UsersController(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }
  [HttpPost("login")]
  public async Task<IActionResult> Login(string userName, string password)
  {
    var user = await _userManager.FindByNameAsync(userName);
    if (user == null)
    {
      return Unauthorized();
    }
    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
    if (!isPasswordCorrect)
    {
      return Unauthorized();
    }
    return Ok();
  }
}
