using Microsoft.AspNetCore.Mvc;

namespace Profio.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ResultsController : ControllerBase
{
  [HttpGet("not-found")]
  public IActionResult GetNotFound()
        => NotFound();
  [HttpGet("ok")]
  public IActionResult GetOk()
      => Ok();
  [HttpGet("bad-request")]
  public IActionResult GetBadRequest()
      => BadRequest();
  [HttpGet("no-content")]
  public IActionResult GetNoContent()
      => NoContent();
  [HttpGet("unauthorized")]
  public IActionResult GetUnauthorized()
      => Unauthorized();
  [HttpGet("conflict")]
  public IActionResult GetConflict()
      => Conflict();
  [HttpGet("forbid")]
  public IActionResult GetForbid()
      => Forbid();
  [HttpGet("internal-server-error")]
  public IActionResult GetInternalServerError()
      => StatusCode(StatusCodes.Status500InternalServerError);
}
