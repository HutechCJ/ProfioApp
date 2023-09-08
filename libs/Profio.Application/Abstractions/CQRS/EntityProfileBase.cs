using AutoMapper;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.Abstractions.CQRS;

public abstract class EntityProfileBase<TEntity, TModel, TCreateCommand, TUpdateCommand> : Profile
  where TEntity : IEntity<object>
  where TModel : BaseModel
  where TCreateCommand : CreateCommandBase
  where TUpdateCommand : UpdateCommandBase
{
  protected EntityProfileBase()
  {
    CreateMap<TEntity, TModel>().ReverseMap();
    CreateMap<TCreateCommand, TEntity>();
    CreateMap<TUpdateCommand, TEntity>()
      .ForAllMembers(options => options.Condition((_, _, srcValue, _) => srcValue != null));
  }
}
