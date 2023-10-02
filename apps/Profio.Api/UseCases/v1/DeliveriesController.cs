using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Deliveries;
using Profio.Application.Deliveries.Commands;
using Profio.Application.Deliveries.Queries;
using Profio.Application.OrderHistories;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Key;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage deliveries")]
public sealed class DeliveriesController : BaseEntityController<Delivery, DeliveryDto, GetDeliveryByIdQuery>
{
  [HttpGet]
  [SwaggerOperation("Get Delivery List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<DeliveryDto>>>> Get([FromQuery] Specification specification)
    => HandlePaginationQuery(new GetDeliveryWithPagingQuery(specification));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation("Get Delivery by Id")]
  public Task<ActionResult<ResultModel<DeliveryDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation("Create Delivery")]
  public Task<ActionResult<ResultModel<DeliveryDto>>> Post(CreateDeliveryCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation("Update Delivery")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateDeliveryCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation("Delete Delivery")]
  public Task<ActionResult<ResultModel<DeliveryDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteDeliveryCommand(id));

  [HttpGet("count")]
  [SwaggerOperation("Get Delivery count")]
  public async Task<ActionResult<ResultModel<int>>> GetCount()
    => Ok(ResultModel<int>.Create(await Mediator.Send(new GetDeliveryCountQuery())));

  [HttpDelete("purge")]
  [SwaggerOperation("Purge Delivery")]
  [ApiKey]
  public async Task<IActionResult> Purge()
  {
    await Mediator.Send(new PurgeDeliveryCommand());
    return NoContent();
  }

  [Obsolete("Deprecated")]
  [HttpGet("{id:length(26)}/orderhistories")]
  [SwaggerOperation("Get Order History List by Delivery Id")]
  public async Task<ActionResult<ResultModel<IPagedList<OrderHistoryDto>>>> GetOrderHistoriesByDeliveryId(string id,
    [FromQuery] Specification specification)
    => Ok(ResultModel<IPagedList<OrderHistoryDto>>.Create(
      await Mediator.Send(new GetOrderHistoryByDeliveryIdWithPagingQuery(id, specification))));
}
