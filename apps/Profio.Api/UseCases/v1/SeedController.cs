using Microsoft.AspNetCore.Mvc;
using Profio.Application.Seed.Commands;
using Profio.Application.Seed.Queries;
using Profio.Domain.Models;
using Profio.Infrastructure.Key;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Api.UseCases.v1;

[ApiVersion("1.0")]
[SwaggerTag("Seed Data")]
public sealed class SeedController : BaseController
{
  [ApiKey]
  [HttpPost]
  [SwaggerOperation(summary: "Seed Data")]
  public async Task<ActionResult<ResultModel<string>>> SeedData()
    => Ok(ResultModel<string>.Create(await Mediator.Send(new SeedDataCommand())));

  [ApiKey]
  [HttpPost]
  [SwaggerOperation(summary: "Seed License Plate")]
  public async Task<ActionResult<ResultModel<string>>> SeedLicensePlate()
    => Ok(ResultModel<string>.Create(await Mediator.Send(new SeedLicensePlateCommand())));
}
