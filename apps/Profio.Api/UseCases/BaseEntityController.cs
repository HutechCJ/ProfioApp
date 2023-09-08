using EntityFrameworkCore.Repository.Collections;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Api.UseCases;

public class BaseEntityController<TEntity, TModel, TGetByIdQuery> : BaseController
  where TEntity : class, IEntity<object>
  where TModel : BaseModel
  where TGetByIdQuery : GetByIdQueryBase<TModel>
{
  protected async Task<ActionResult<ResultModel<IPagedList<TModel>>>> HandlePaginationQuery<TPaginationQuery>(TPaginationQuery query)
        where TPaginationQuery : GetWithPagingQueryBase<TEntity, TModel>
        => Ok(ResultModel<IPagedList<TModel>>.Create(await Mediator.Send(query)));
  protected async Task<ActionResult<ResultModel<TModel>>> HandleGetByIdQuery(TGetByIdQuery query)
        => Ok(ResultModel<TModel>.Create(await Mediator.Send(query)));
  protected async Task<ActionResult<ResultModel<TModel>>> HandleCreateCommand<TCreateCommand>(TCreateCommand command)
        where TCreateCommand : CreateCommandBase
  {
    var id = await Mediator.Send(command);
    var query = Activator.CreateInstance(typeof(TGetByIdQuery), id) as TGetByIdQuery ?? throw new InvalidOperationException("Cannot create get by Id query!");
    var model = await Mediator.Send(query);

    var domain = HttpContext.Request.GetDisplayUrl();
    var routeTemplate = ControllerContext.ActionDescriptor.AttributeRouteInfo!.Template;
    var apiVersion = HttpContext.GetRequestedApiVersion()!.ToString();

    return Created($"{domain}/{routeTemplate!.Replace("{version:apiVersion}", apiVersion)}/{id}", ResultModel<TModel>.Create(model));
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
      => Ok(ResultModel<TModel>.Create(await Mediator.Send(command)));
}
