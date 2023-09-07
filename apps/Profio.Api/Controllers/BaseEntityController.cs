using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Events.Queries;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Api.Controllers;

public class BaseEntityController<TEntity, TModel> : BaseController
  where TEntity : class, IEntity<object>
  where TModel : BaseModel
{
  protected async Task<ActionResult<ResultModel<IPagedList<TModel>>>> HandlePaginationQuery<TPaginationQuery>(TPaginationQuery query)
        where TPaginationQuery : GetWithPagingQueryBase<TEntity, TModel>
        => Ok(await Mediator.Send(query));
  protected async Task<ActionResult<ResultModel<TModel>>> HandleGetByIdQuery<TGetQuery>(TGetQuery query)
        where TGetQuery : GetByIdQueryBase<TModel>
        => Ok(await Mediator.Send(query));
  protected async Task<ActionResult<ResultModel<object>>> HandleCreateCommand<TCreateCommand, TQuery>(TCreateCommand command, Func<object, TQuery> getQuery)
        where TCreateCommand : CreateCommandBase
        where TQuery : GetByIdQueryBase<TModel>
  {
    var id = await Mediator.Send(command);
    var model = await Mediator.Send(getQuery(id));

    var domain = HttpContext.Request.GetDisplayUrl();
    var routeTemplate = ControllerContext.ActionDescriptor.AttributeRouteInfo!.Template;
    var apiVersion = HttpContext.GetRequestedApiVersion()!.ToString();

    return Created($"{domain}/{routeTemplate!.Replace("{version:apiVersion}", apiVersion)}/{id}", model);
  }

  protected async Task<IActionResult> HandleUpdateCommand<TUpdateCommand>(string id, TUpdateCommand command)
    where TUpdateCommand : UpdateCommandBase
  {
    if (!id.Equals(command.Id))
    {
      ModelState.AddModelError("Id", "Ids are not the same");
      return ValidationProblem();
    }
    await Mediator.Send(command).ConfigureAwait(false);
    return NoContent();
  }

  protected async Task<ActionResult<ResultModel<TModel>>> HandleDeleteCommand<TDeleteCommand>(TDeleteCommand command)
      where TDeleteCommand : DeleteCommandBase<TModel>
      => Ok(await Mediator.Send(command));
}
