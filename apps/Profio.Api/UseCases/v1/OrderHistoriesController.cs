using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.OrderHistories;
using Profio.Application.OrderHistories.Commands;
using Profio.Application.OrderHistories.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage order histories")]
public sealed class
  OrderHistoriesController : BaseEntityController<OrderHistory, OrderHistoryDto, GetOrderHistoryByIdQuery>
{
  [HttpGet]
  [Obsolete("Deprecated")]
  [SwaggerOperation("Get Order History List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<OrderHistoryDto>>>> Get([FromQuery] Specification specification)
    => HandlePaginationQuery(new GetOrderHistoryWithPagingQuery(specification));

  [HttpGet("{id:length(26)}")]
  [Obsolete("Deprecated")]
  [SwaggerOperation("Get Order History by Id")]
  public Task<ActionResult<ResultModel<OrderHistoryDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [Obsolete("Deprecated")]
  [SwaggerOperation("Create Order History")]
  public Task<ActionResult<ResultModel<OrderHistoryDto>>> Post(CreateOrderHistoryCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [Obsolete("Deprecated")]
  [SwaggerOperation("Update Order History")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateOrderHistoryCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [Obsolete("Deprecated")]
  [SwaggerOperation("Delete Order History")]
  public Task<ActionResult<ResultModel<OrderHistoryDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteOrderHistoryCommand(id));
}
