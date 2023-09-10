using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Customers;
using Profio.Application.Customers.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Profio.Domain.Specifications;

namespace Profio.Api.UseCases.v2;

[ApiVersion("2.0")]
public class CustomerController : BaseEntityController<Customer, CustomerDto, GetCustomerByIdQuery>
{
  [HttpGet]
  [MapToApiVersion("2.0")]
  public Task<ActionResult<ResultModel<IPagedList<CustomerDto>>>> Get([FromQuery] Criteria criteria)
    => HandlePaginationQuery(new GetCustomerWithPagingQuery(criteria));
}
