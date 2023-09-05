using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[AllowAnonymous]
[SwaggerTag("Check the results of the HTTP status code")]
public class ResultsController : BaseController
{
  [HttpGet("not-found")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetNotFound()
        => NotFound();

  [HttpGet("ok")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetOk()
      => Ok();

  [HttpGet("bad-request")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetBadRequest()
      => BadRequest();

  [HttpGet("no-content")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetNoContent()
      => NoContent();

  [HttpGet("unauthorized")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetUnauthorized()
      => Unauthorized();

  [HttpGet("conflict")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetConflict()
      => Conflict();

  [HttpGet("forbid")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetForbid()
      => Forbid();

  [HttpGet("internal-server-error")]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetInternalServerError()
      => StatusCode(StatusCodes.Status500InternalServerError);
}
