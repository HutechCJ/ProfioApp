using Microsoft.AspNetCore.Mvc;
using Profio.Application.Customers;
using Profio.Application.Customers.Commands;
using Profio.Domain.Entities;
using Profio.Domain.Models;

namespace Profio.Api.Controllers.v1;

[ApiVersion("1.0")]
public class CustomersController : BaseEntityController<Customer, CustomerDTO>
{
  [HttpPost]
  [MapToApiVersion("1.0")]
  public Task<ActionResult<ResultModel<object>>> Post(CreateCustomerCommand command)
    => HandleCreateCommand(command);
}
