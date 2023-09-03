using AutoMapper;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Profio.Application.CQRS.Events.Commands;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.CQRS.Handlers.Command;

public class CreateCommandHandlerBase<TCommand, TEntity> : IRequestHandler<TCommand, ResultModel<object>>
  where TCommand : CreateCommandBase
  where TEntity : class, IEntity<object>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  public CreateCommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper) => (_unitOfWork, _mapper, _repository) = (unitOfWork, mapper, unitOfWork.Repository<TEntity>());
  public async Task<ResultModel<object>> Handle(TCommand request, CancellationToken cancellationToken)
  {
    var entity = _mapper.Map<TEntity>(request);

    await _repository.AddAsync(entity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

    _repository.RemoveTracking(entity);

    return ResultModel<object>.Create(entity.Id);
  }
}
