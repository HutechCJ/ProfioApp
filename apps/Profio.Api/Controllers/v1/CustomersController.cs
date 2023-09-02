using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Customers.Commands;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
public class CustomersController : BaseController
{
  [HttpPost]
  [MapToApiVersion("1.0")]
  public async Task<IActionResult> Post(CreateCustomerCommand command)
  {
    var result = await Mediator.Send(command);

    var domain = HttpContext.Request.GetDisplayUrl();
    var routeTemplate = ControllerContext.ActionDescriptor.AttributeRouteInfo!.Template;
    var apiVersion = HttpContext.GetRequestedApiVersion()!.ToString();

    return Created($"{domain}/{routeTemplate!.Replace("{version:apiVersion}", apiVersion)}/{result.Data}", result);
  }
}
