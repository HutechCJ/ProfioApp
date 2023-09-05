using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Hubs;
using Profio.Application.Hubs.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;
namespace Profio.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("A hub endpoints")]
    public class HubController : BaseEntityController<Hub, HubDto>
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public Task<ActionResult<ResultModel<IPagedList<HubDto>>>> Get([FromQuery] Criteria<Hub> criteria)
            => HandlePaginationQuery(new GetHubWithPagingQuery(criteria));
    }
}
