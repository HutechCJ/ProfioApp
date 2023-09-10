using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Orders;
using Profio.Application.Orders.Commands;
using Profio.Application.Orders.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage orders")]
public class OrdersController : BaseEntityController<Order, OrderDto, GetOrderByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<OrderDto>>>> Get([FromQuery] Criteria criteria)
    => HandlePaginationQuery(new GetOrderWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<OrderDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<OrderDto>>> Post(CreateOrderCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateOrderCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<OrderDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteOrderCommand(id));
}
