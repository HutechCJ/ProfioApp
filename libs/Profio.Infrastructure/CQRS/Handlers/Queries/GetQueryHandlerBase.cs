using AutoMapper;
using MediatR;
using Profio.Domain.Interfaces;
using Profio.Infrastructure.CQRS.Events.Queries;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Handlers.Queries;

public abstract class GetQueryHandlerBase<TQuery, TModel, TEntity> : IRequestHandler<TQuery, ListResultModel<TModel>>
  where TQuery : GetQueryByFieldBase<TModel>
  where TModel : BaseModel
  where TEntity : class
{
  private readonly IRelationalRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  protected GetQueryHandlerBase(IUnitOfWork<TEntity> unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository, mapper);

  public async Task<ListResultModel<TModel>> Handle(TQuery request, CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(request);
    var result = await _repository.GetAllAsync(cancellationToken);
    return new(_mapper.Map<List<TModel>>(result));
  }
}
