using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Customers.Queries;
using Profio.Application.Deliveries;
using Profio.Application.Hubs;
using Profio.Application.Orders;
using Profio.Application.Orders.Commands;
using Profio.Application.Orders.Queries;
using Profio.Application.Vehicles;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage orders")]
public sealed class OrdersController : BaseEntityController<Order, OrderDto, GetOrderByIdQuery>
{
  [HttpGet]
  [SwaggerOperation("Get Order List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<OrderDto>>>> Get([FromQuery] Criteria criteria,
    [FromQuery] OrderEnumFilter orderEnumFilter)
    => HandlePaginationQuery(new GetOrderWithPagingQuery(criteria, orderEnumFilter));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation("Get Order by Id")]
  public Task<ActionResult<ResultModel<OrderDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation("Create Order")]
  public Task<ActionResult<ResultModel<OrderDto>>> Post(CreateOrderCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation("Update Order")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateOrderCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation("Delete Order")]
  public Task<ActionResult<ResultModel<OrderDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteOrderCommand(id));

  [HttpGet("count")]
  [SwaggerOperation("Get Order count")]
  public async Task<ActionResult<ResultModel<int>>> GetCount()
    => Ok(ResultModel<int>.Create(await Mediator.Send(new GetOrderCountQuery())));

  [HttpGet("count-by-status")]
  [SwaggerOperation("Get Order count by Status")]
  public async Task<ActionResult<ResultModel<IEnumerable<int>>>> GetCountByStatus()
    => Ok(ResultModel<IEnumerable<int>>.Create(await Mediator.Send(new GetOrderCountByStatusQuery())));

  [HttpGet("{id:length(26)}/vehicles/available")]
  [SwaggerOperation("Get available Vehicle List by Order Id")]
  public async Task<ActionResult<ResultModel<IPagedList<VehicleDto>>>> GetAvailableVehicles(string id,
    [FromQuery] Criteria criteria)
    => Ok(ResultModel<IPagedList<VehicleDto>>.Create(
      await Mediator.Send(new GetAvailableVehicleByOrderIdWithPagingQuery(id, criteria))));

  [HttpGet("{id:length(26)}/deliveries")]
  [SwaggerOperation("Get Delivery List by Order Id")]
  public async Task<ActionResult<ResultModel<IPagedList<DeliveryDto>>>> GetDeliveriesByOrderId(string id,
    [FromQuery] Criteria criteria)
    => Ok(ResultModel<IPagedList<DeliveryDto>>.Create(
      await Mediator.Send(new GetDeliveryByOrderIdWithPagingQuery(id, criteria))));

  [HttpGet("{id:length(26)}/hubs/path")]
  [SwaggerOperation("Get Hub path by Order Id")]
  public async Task<ActionResult<ResultModel<IPagedList<HubDto>>>> GetHubPath(string id, [FromQuery] Criteria criteria)
    => Ok(ResultModel<IPagedList<HubDto>>.Create(await Mediator.Send(new GetHubPathByOrderIdQuery(id, criteria))));

  [HttpGet("lookup")]
  [SwaggerOperation("Get Order List By Phone number with Paging")]
  public async Task<ActionResult<ResultModel<IPagedList<OrderDto>>>> GetOrderByPhoneNumber([FromQuery] string phone,
    [FromQuery] Criteria criteria, [FromQuery] OrderEnumFilter orderEnumFilter, [FromQuery] bool current)
    => Ok(ResultModel<IPagedList<OrderDto>>.Create(
      await Mediator.Send(
        new GetOrderByCustomerPhoneNumberWithPagingQuery(phone, criteria, orderEnumFilter, current))));
}
