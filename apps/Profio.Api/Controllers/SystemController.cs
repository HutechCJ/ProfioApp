using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Infrastructure.System;

namespace Profio.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SystemController : ControllerBase
{
  private readonly IConfiguration _config;
  private readonly IWebHostEnvironment _env;

  public SystemController(IConfiguration config, IWebHostEnvironment env)
    => (_config, _env) = (config, env);

  [HttpGet]
  [AllowAnonymous]
  public IActionResult GetPlatform()
    => Ok(_config.GetPlatform(_env));

  [HttpGet("status")]
  [AllowAnonymous]
  public IActionResult GetServerStatus()
    => Ok(Extension.GetPlatformStatus(_env));
}
