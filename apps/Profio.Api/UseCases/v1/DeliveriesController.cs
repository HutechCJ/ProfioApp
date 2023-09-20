using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Deliveries;
using Profio.Application.Deliveries.Commands;
using Profio.Application.Deliveries.Queries;
using Profio.Application.OrderHistories;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage deliveries")]
public sealed class DeliveriesController : BaseEntityController<Delivery, DeliveryDto, GetDeliveryByIdQuery>
{
  [HttpGet]
  [SwaggerOperation(summary: "Get Delivery List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<DeliveryDto>>>> Get([FromQuery] Criteria criteria)
    => HandlePaginationQuery(new GetDeliveryWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation(summary: "Get Delivery by Id")]
  public Task<ActionResult<ResultModel<DeliveryDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation(summary: "Create Delivery")]
  public Task<ActionResult<ResultModel<DeliveryDto>>> Post(CreateDeliveryCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation(summary: "Update Delivery")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateDeliveryCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation(summary: "Delete Delivery")]
  public Task<ActionResult<ResultModel<DeliveryDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteDeliveryCommand(id));
  [HttpGet("count")]
  [SwaggerOperation(summary: "Get Delivery count")]
  public async Task<ActionResult<ResultModel<int>>> GetCount()
    => Ok(ResultModel<int>.Create(await Mediator.Send(new GetDeliveryCountQuery())));

  [Obsolete("Depricated")]
  [HttpGet("{id:length(26)}/orderhistories")]
  [SwaggerOperation(summary: "Get Order History List by Delivery Id")]
  public async Task<ActionResult<ResultModel<IPagedList<OrderHistoryDto>>>> GetOrderHistoriesByDeliveryId(string id, [FromQuery] Criteria criteria)
    => Ok(ResultModel<IPagedList<OrderHistoryDto>>.Create(await Mediator.Send(new GetOrderHistoryByDeliveryIdWithPagingQuery(id, criteria))));

}
