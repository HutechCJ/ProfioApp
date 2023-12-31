using Microsoft.AspNetCore.Mvc;
using Profio.Application.Statistical.Queries;
using Profio.Domain.Entities;
using Profio.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;
using Route = Profio.Domain.Entities.Route;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manage counters")]
public sealed class CountersController : BaseController
{
  [HttpGet("entities")]
  [SwaggerOperation("Retrieve the count of items in each table of the entity")]
  public async Task<ActionResult<ResultModel<Dictionary<string, int>>>> GetEntityCount(
    [FromQuery]
    [SwaggerParameter(
      $"{nameof(Customer)}, {nameof(Delivery)}, {nameof(DeliveryProgress)}, {nameof(Incident)}, {nameof(Order)}, {nameof(OrderHistory)}, {nameof(Route)}, {nameof(Staff)}, {nameof(Vehicle)}",
      Required = true)]
    IList<string> entityTypes)
    => Ok(ResultModel<Dictionary<string, int>>.Create(await Mediator.Send(new GetEntityCountQuery(entityTypes))));
}
