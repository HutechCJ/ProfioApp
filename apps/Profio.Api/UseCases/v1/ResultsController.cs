using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Domain.Exceptions;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[AllowAnonymous]
[SwaggerTag("Check the results of the HTTP status code")]
public sealed class ResultsController : BaseController
{
  [HttpGet("not-found")]
  [SwaggerOperation("Get Not Found result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetNotFound()
    => throw new NotFoundException("Not found Result");

  [HttpGet("ok")]
  [SwaggerOperation("Get Ok result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetOk()
    => Ok();

  [HttpGet("bad-request")]
  [SwaggerOperation("Get Bad Request result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetBadRequest()
    => BadRequest();

  [HttpGet("no-content")]
  [SwaggerOperation("Get No Content result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetNoContent()
    => NoContent();

  [HttpGet("unauthorized")]
  [SwaggerOperation("Get Unauthorized result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetUnauthorized()
    => Unauthorized();

  [HttpGet("conflict")]
  [SwaggerOperation("Get Conflict result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetConflict()
    => Conflict();

  [HttpGet("forbid")]
  [SwaggerOperation("Get Forbid result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetForbid()
    => Forbid();

  [HttpGet("internal-server-error")]
  [SwaggerOperation("Get Internal Server Error result")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetInternalServerError()
    => StatusCode(StatusCodes.Status500InternalServerError);
}
