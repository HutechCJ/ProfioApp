using Microsoft.AspNetCore.Mvc;
using Profio.Application.Seed.Queries;

namespace Profio.Api.Controllers.v1
{
    public class SeedController : BaseController
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetNotFoundAsync()
            => Ok(await Mediator.Send(new SeedDataQuery()));
    }
}