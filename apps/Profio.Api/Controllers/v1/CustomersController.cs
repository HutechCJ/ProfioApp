using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Customers;
using Profio.Application.Customers.Commands;
using Profio.Application.Customers.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage customers")]
public class CustomersController : BaseEntityController<Customer, CustomerDto, GetCustomerByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<CustomerDto>>>> Get([FromQuery] Criteria<Customer> criteria)
    => HandlePaginationQuery(new GetCustomerWithPagingQuery(criteria));

  [HttpGet("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<CustomerDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetCustomerByIdQuery(id));

  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<CustomerDto>>> Post(CreateCustomerCommand command)
    => HandleCreateCommand(command);

  [HttpPut("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateCustomerCommand command)
    => HandleUpdateCommand(id, command);

  [HttpDelete("{id:length(26)}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<CustomerDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteCustomerCommand(id));
}
