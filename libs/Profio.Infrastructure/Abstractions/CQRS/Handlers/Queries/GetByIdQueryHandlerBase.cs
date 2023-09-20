using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Exceptions;

namespace Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;

public abstract class GetByIdQueryHandlerBase<TQuery, TModel, TEntity> : IRequestHandler<TQuery, TModel>
  where TQuery : GetByIdQueryBase<TModel>
  where TModel : BaseModel
  where TEntity : class, IEntity<object>
{
  private readonly IMapper _mapper;
  private readonly IRepository<TEntity> _repository;

  protected GetByIdQueryHandlerBase(IRepositoryFactory unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository<TEntity>(), mapper);

  public async Task<TModel> Handle(TQuery request, CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(request);

    var query = _repository
      .SingleResultQuery()
      .AndFilter(x => x.Id.Equals(request.Id));

    var data = await _repository
      .ToQueryable(query)
      .AsSplitQuery()
      .ProjectTo<TModel>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(TEntity).Name, request.Id);

    return data;
  }
}
