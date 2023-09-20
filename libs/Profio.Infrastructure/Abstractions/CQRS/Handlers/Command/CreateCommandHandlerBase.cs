using AutoMapper;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Profio.Domain.Interfaces;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;

namespace Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;

public class CreateCommandHandlerBase<TCommand, TEntity> : IRequestHandler<TCommand, object>
  where TCommand : CreateCommandBase
  where TEntity : class, IEntity<object>
{
  private readonly IMapper _mapper;
  private readonly IRepository<TEntity> _repository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateCommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper) => (_unitOfWork, _mapper, _repository) =
    (unitOfWork, mapper, unitOfWork.Repository<TEntity>());

  public async Task<object> Handle(TCommand request, CancellationToken cancellationToken)
  {
    var entity = _mapper.Map<TEntity>(request);

    await _repository.AddAsync(entity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

    _repository.RemoveTracking(entity);

    return entity.Id;
  }
}
