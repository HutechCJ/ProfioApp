using Microsoft.AspNetCore.Mvc;
using Profio.Application.Customers;
using Profio.Application.Customers.Commands;
using Profio.Application.Customers.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage customers")]
public class CustomersController : BaseEntityController<Customer, CustomerDto>
{
  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<object>>> Post(CreateCustomerCommand command)
    => HandleCreateCommand(command, id => new GetCustomerByIdQuery(id));
}
