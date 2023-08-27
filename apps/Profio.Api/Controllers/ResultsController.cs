using Microsoft.AspNetCore.Mvc;

namespace Profio.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class ResultsController : ControllerBase
{
  [HttpGet("not-found")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetNotFound()
        => NotFound();

  [HttpGet("ok")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetOk()
      => Ok();

  [HttpGet("bad-request")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetBadRequest()
      => BadRequest();

  [HttpGet("no-content")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetNoContent()
      => NoContent();

  [HttpGet("unauthorized")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetUnauthorized()
      => Unauthorized();

  [HttpGet("conflict")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetConflict()
      => Conflict();

  [HttpGet("forbid")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetForbid()
      => Forbid();

  [HttpGet("internal-server-error")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetInternalServerError()
      => StatusCode(StatusCodes.Status500InternalServerError);
}
