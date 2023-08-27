using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Models;
using Profio.Domain.Interfaces;

namespace Profio.Application.CQRS.Handlers.Queries;

public abstract class GetQueryHandlerBase<TQuery, TModel, TEntity> : IRequestHandler<TQuery, ListResultModel<TModel>>
  where TQuery : GetByFieldQueryBase<TModel>
  where TModel : BaseModel
  where TEntity : class, IEntity<object>
{
  private readonly IRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  protected GetQueryHandlerBase(IRepositoryFactory unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository<TEntity>(), mapper);

  public async Task<ListResultModel<TModel>> Handle(TQuery request, CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(request);

    var query = _repository
      .MultipleResultQuery();

    var data = await _repository
      .ToQueryable(query)
      .AsSplitQuery()
      .ProjectTo<TModel>(_mapper.ConfigurationProvider)
      .ToListAsync(cancellationToken);

    return new(data);
  }
}
