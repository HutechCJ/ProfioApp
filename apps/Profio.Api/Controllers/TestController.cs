using Microsoft.AspNetCore.Mvc;
using Profio.Domain.Graph.Nodes;
using Profio.Domain.Interfaces;

namespace Profio.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
  private readonly IGraphOfWork _graphOfWork;

  public TestController(IGraphOfWork graphOfWork) => _graphOfWork = graphOfWork;

  [HttpPost]
  public async Task<IActionResult> Post()
  {
    var owner = new Owner { Name = "Profio" };
    await _graphOfWork.Owner.CreateAsync("(n:Owner $node)", owner);
    return Ok(owner);
  }
}
