using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Profio.Domain.Constants;
using Profio.Domain.Models;
using Profio.Infrastructure.Filters;
using Profio.Infrastructure.Key;
using Profio.Infrastructure.System;
using Swashbuckle.AspNetCore.Annotations;
using Extension = Profio.Infrastructure.System.Extension;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[Authorize(Roles = UserRole.Administrator)]
[SwaggerTag("The System Monitor and Status")]
public sealed class SystemController : BaseController
{
  private readonly IConfiguration _config;
  private readonly IWebHostEnvironment _env;

  public SystemController(IConfiguration config, IWebHostEnvironment env)
    => (_config, _env) = (config, env);

  [ApiKey]
  [HttpGet]
  [SwaggerOperation(summary: "Get Platform's configuration",
    Description = "Only `IP` in `SafeList` can access this endpoint")]
  [ServiceFilter(typeof(ClientIpCheckActionFilter))]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public ActionResult<ResultModel<JObject>> GetPlatform()
    => Ok(ResultModel<JObject>.Create(_config.GetPlatform(_env)));

  [ApiKey]
  [HttpGet("status")]
  [ServiceFilter(typeof(ClientIpCheckActionFilter))]
  [SwaggerOperation(summary: "Get Platform's status",
    Description = "**Warming:** Only `IP` in `SafeList` can access this endpoint. In production, Azure has been `BLOCKED` to access this endpoint")]
  public ActionResult<ResultModel<JObject>> GetServerStatus()
    => Ok(ResultModel<JObject>.Create(Extension.GetPlatformStatus(_env)));
}
