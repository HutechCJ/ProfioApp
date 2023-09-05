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
public class CustomersController : BaseEntityController<Customer, CustomerDto>
{
  [HttpGet]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<IPagedList<CustomerDto>>>> Get([FromQuery] Criteria<Customer> criteria)
    => HandlePaginationQuery(new GetCustomerWithPagingQuery(criteria));
  [HttpGet("{id}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<CustomerDto>>> GetById(string id)
    => HandleGetByIdQuery(new GetCustomerByIdQuery(id));
  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<object>>> Post(CreateCustomerCommand command)
    => HandleCreateCommand(command, id => new GetCustomerByIdQuery(id));
  [HttpPut("{id}")]
  [MapToApiVersion("1.0")]
  public Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateCustomerCommand command)
    => HandleUpdateCommand(id, command);
  [HttpDelete("{id}")]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<CustomerDto>>> Delete(string id)
    => HandleDeleteCommand(new DeleteCustomerCommand(id));
}
