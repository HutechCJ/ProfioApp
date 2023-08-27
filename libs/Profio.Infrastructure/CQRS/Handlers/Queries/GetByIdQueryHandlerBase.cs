using AutoMapper;
using MediatR;
using Profio.Domain.Interfaces;
using Profio.Infrastructure.CQRS.Events.Queries;
using Profio.Infrastructure.CQRS.Models;

namespace Profio.Infrastructure.CQRS.Handlers.Queries;

public abstract class GetByIdQueryHandlerBase<TQuery, TModel, TEntity> : IRequestHandler<TQuery, ResultModel<TModel>>
  where TQuery : GetByIdQueryBase<TModel>
  where TModel : BaseModel
  where TEntity : class
{
  private readonly IRelationalRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  protected GetByIdQueryHandlerBase(IUnitOfWork<TEntity> unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository, mapper);

  public async Task<ResultModel<TModel>> Handle(TQuery request, CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(request);
    var result = await _repository.GetByIdAsync(request.Id, cancellationToken);
    return new(_mapper.Map<TModel>(result));
  }
}
