using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.DeliveryProgresses;
using Profio.Application.DeliveryProgresses.Commands;
using Profio.Application.DeliveryProgresses.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage delivery progresses")]
public class DeliveryProgressesController : BaseEntityController<DeliveryProgress, DeliveryProgressDto, GetDeliveryProgressByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<DeliveryProgressDto>>>> Get([FromQuery] Criteria<DeliveryProgress> criteria)
    => HandlePaginationQuery(new GetDeliveryProgressWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<DeliveryProgressDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetDeliveryProgressByIdQuery(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<DeliveryProgressDto>>> Post(CreateDeliveryProgressCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateDeliveryProgressCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<DeliveryProgressDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteDeliveryProgressCommand(id));
}
