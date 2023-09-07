using AutoMapper;
using Profio.Application.CQRS.Events.Commands;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.CQRS;

public abstract class EntityProfileBase<TEntity, TModel, TCreateCommand, TUpdateCommand> : Profile
  where TEntity : IEntity<object>
  where TModel : BaseModel
  where TCreateCommand : CreateCommandBase
  where TUpdateCommand : UpdateCommandBase
{
  public EntityProfileBase()
  {
    CreateMap<TEntity, TModel>().ReverseMap();
    CreateMap<TCreateCommand, TEntity>();
    CreateMap<TUpdateCommand, TEntity>()
      .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
  }
}
