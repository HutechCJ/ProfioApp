using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Infrastructure.System;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[SwaggerTag("The System Monitor and Status")]
public class SystemController : ControllerBase
{
  private readonly IConfiguration _config;
  private readonly IWebHostEnvironment _env;

  public SystemController(IConfiguration config, IWebHostEnvironment env)
    => (_config, _env) = (config, env);

  [HttpGet]
  [AllowAnonymous]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetPlatform()
    => Ok(_config.GetPlatform(_env));

  [HttpGet("status")]
  [AllowAnonymous]
  public IActionResult GetServerStatus()
    => Ok(Extension.GetPlatformStatus(_env));
}
