using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Exceptions;
using Profio.Domain.Interfaces;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;

namespace Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;

public class UpdateCommandHandlerBase<TCommand, TEntity> : IRequestHandler<TCommand, Unit>
  where TEntity : class, IEntity<object>
  where TCommand : UpdateCommandBase
{
  private readonly IMapper _mapper;
  private readonly IRepository<TEntity> _repository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateCommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _repository = _unitOfWork.Repository<TEntity>();
    _mapper = mapper;
  }

  public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
  {
    var query = _repository.SingleResultQuery()
      .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
      .AndFilter(m => m.Id.Equals(request.Id));

    var entity = await _repository.FirstOrDefaultAsync(query, cancellationToken)
                 ?? throw new NotFoundException(typeof(TEntity).Name, request.Id);

    _mapper.Map(request, entity);

    await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
      .ConfigureAwait(false);

    return Unit.Value;
  }
}
