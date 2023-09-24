using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Customers;
using Profio.Application.Customers.Commands;
using Profio.Application.Customers.Queries;
using Profio.Application.Orders;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage customers")]
public sealed class CustomersController : BaseEntityController<Customer, CustomerDto, GetCustomerByIdQuery>
{
  [HttpGet]
  [SwaggerOperation(summary: "Get Customer List with Paging")]
  public Task<ActionResult<ResultModel<IPagedList<CustomerDto>>>> Get([FromQuery] Criteria criteria, [FromQuery] CustomerEnumFilter orderEnumFilter)
    => HandlePaginationQuery(new GetCustomerWithPagingQuery(criteria, orderEnumFilter));

  [HttpGet("{id:length(26)}")]
  [SwaggerOperation(summary: "Get Customer by Id")]
  public Task<ActionResult<ResultModel<CustomerDto>>> GetById(string id)
    => HandleGetByIdQuery(new(id));

  [HttpPost]
  [SwaggerOperation(summary: "Create Customer")]
  public Task<ActionResult<ResultModel<CustomerDto>>> Post(CreateCustomerCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [SwaggerOperation(summary: "Update Customer")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateCustomerCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [SwaggerOperation(summary: "Delete Customer")]
  public Task<ActionResult<ResultModel<CustomerDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteCustomerCommand(id));

  [HttpGet("{id:length(26)}/orders")]
  [SwaggerOperation(summary: "Get Order List by Customer Id with Paging")]
  public async Task<ActionResult<ResultModel<IPagedList<CustomerDto>>>> Get(string id, [FromQuery] Criteria criteria, [FromQuery] OrderEnumFilter orderEnumFilter)
    => Ok(ResultModel<IPagedList<OrderDto>>.Create(await Mediator.Send(new GetOrderByCustomerIdWithPagingQuery(id, criteria, orderEnumFilter))));
}
