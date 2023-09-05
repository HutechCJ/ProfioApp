using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Profio.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
  private IMediator? _mediator;
  protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                                                ?? throw new NullReferenceException(nameof(_mediator));
}
