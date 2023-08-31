using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Profio.Application.CQRS.Events.Queries;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.CQRS.Handlers.Queries;

public abstract class GetWithPagingQueryHandler<TQuery, TModel, TEntity> : IRequestHandler<TQuery, IPagedList<TModel>>
  where TQuery : GetWithPagingQueryBase<TModel>
  where TModel : BaseModel
  where TEntity : class, IEntity<object>
{
  private readonly IRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  protected GetWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository<TEntity>(), mapper);

  public Task<IPagedList<TModel>> Handle(TQuery request, CancellationToken cancellationToken)
    => throw new NotImplementedException();
}
