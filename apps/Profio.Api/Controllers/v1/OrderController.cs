using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Orders.Commands;
using Profio.Application.Orders;
using Profio.Application.Orders.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage orders")]
public class OrderController : BaseEntityController<Order, OrderDto>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<OrderDto>>>> Get([FromQuery] Criteria<Order> criteria)
    => HandlePaginationQuery(new GetOrderWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<OrderDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetOrderByIdQuery(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<object>>> Post(CreateOrderCommand command)
    => HandleCreateCommand(command, id => new GetOrderByIdQuery(id));

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateOrderCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<OrderDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteOrderCommand(id));
}
