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

public abstract class GetByIdQueryHandlerBase<TQuery, TModel, TEntity> : IRequestHandler<TQuery, ResultModel<TModel>>
  where TQuery : GetByIdQueryBase<TModel>
  where TModel : BaseModel
  where TEntity : class, IEntity<object>
{
  private readonly IRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  protected GetByIdQueryHandlerBase(IRepositoryFactory unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository<TEntity>(), mapper);

  public async Task<ResultModel<TModel>> Handle(TQuery request, CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(request);

    var query = _repository
      .SingleResultQuery()
      .AndFilter(x => x.Id.Equals(request.Id));

    var data = await _repository
      .ToQueryable(query)
      .AsSplitQuery()
      .ProjectTo<TModel>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync(cancellationToken);

    return new(data);
  }
}
