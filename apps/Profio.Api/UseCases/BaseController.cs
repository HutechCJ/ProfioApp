using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Profio.Api.UseCases;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class BaseController : ControllerBase
{
  private IMediator? _mediator;
  protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                                                ?? throw new NullReferenceException(nameof(_mediator));
}
