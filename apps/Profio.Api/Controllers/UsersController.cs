using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Users.Commands.Login;
using Profio.Application.Users.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
[SwaggerTag("An authenticated and authorized user")]
public class UsersController : ControllerBase
{
  private readonly IMediator _mediator;

  public UsersController(IMediator mediator)
    => _mediator = mediator;

  [HttpPost("login")]
  public async Task<IActionResult> Login(string userName, string password)
  {
    await _mediator.Send(new LoginCommand(userName, password));
    return Ok();
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById(string id)
  {
    var result = await _mediator.Send(new GetUserByIdQuery(id));
    return Ok(result);
  }
}
