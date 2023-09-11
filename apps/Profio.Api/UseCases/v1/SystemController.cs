using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profio.Domain.Constants;
using Profio.Infrastructure.Key;
using Profio.Infrastructure.System;
using Swashbuckle.AspNetCore.Annotations;
using Extension = Profio.Infrastructure.System.Extension;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[Authorize(Roles = UserRole.Administrator)]
[SwaggerTag("The System Monitor and Status")]
public class SystemController : BaseController
{
  private readonly IConfiguration _config;
  private readonly IWebHostEnvironment _env;

  public SystemController(IConfiguration config, IWebHostEnvironment env)
    => (_config, _env) = (config, env);

  [ApiKey]
  [HttpGet]
  [MapToApiVersion("1.0")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult GetPlatform()
    => Ok(_config.GetPlatform(_env));

  [ApiKey]
  [HttpGet("status")]
  [MapToApiVersion("1.0")]
  public IActionResult GetServerStatus()
    => Ok(Extension.GetPlatformStatus(_env));
}
