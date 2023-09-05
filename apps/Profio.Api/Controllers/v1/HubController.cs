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

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public Task<ActionResult<ResultModel<HubDto>>> GetById(string id)
            => HandleGetByIdQuery(new GetHubByIdQuery(id));
        
        // [HttpPost]
        // [MapToApiVersion("1.0")]
        // public Task<ActionResult<ResultModel<object>>> Post(CreateHubCommand command)
        //     => HandleCreateCommand(command, id => new GetHubByIdQuery(id));
        
        // [HttpPut("{id}")]
        // [MapToApiVersion("1.0")]
        // public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateHubCommand command)
        //     => HandleUpdateCommand(id, command);
        
        // [HttpDelete("{id}")]
        // [MapToApiVersion("1.0")]
        // public Task<ActionResult<ResultModel<HubDto>>> Delete(string id)
        //     => HandleDeleteCommand(new DeleteHubCommand(id));
    }
}
