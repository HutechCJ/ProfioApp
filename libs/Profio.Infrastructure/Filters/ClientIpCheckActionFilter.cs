using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Profio.Infrastructure.Filters;

public class ClientIpCheckActionFilter : ActionFilterAttribute
{
  private readonly ILogger<ClientIpCheckActionFilter> _logger;
  private readonly string? _safeList;

  public ClientIpCheckActionFilter(ILogger<ClientIpCheckActionFilter> logger, string? safeList)
    => (_logger, _safeList) = (logger, safeList);

  public override void OnActionExecuting(ActionExecutingContext context)
  {
    var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
    _logger.LogInformation("Remote IP address: {remoteIp}", remoteIp);

    var ip = _safeList?.Split(';');

    if (remoteIp is { IsIPv4MappedToIPv6: true })
    {
      remoteIp.MapToIPv4();
    }

    var badIp = ip is { } && !ip.Select(IPAddress.Parse).Contains(remoteIp);

    if (badIp)
    {
      _logger.LogInformation("Forbidden Request from Remote IP address: {remoteIp}", remoteIp);
      context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
      return;
    }

    base.OnActionExecuting(context);
  }
}
