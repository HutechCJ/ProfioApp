using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.CQRS.Events.Commands;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;
using Profio.Infrastructure.Excepitions;

namespace Profio.Application.CQRS.Handlers.Command;

public class UpdateCommandHandlerBase<TCommand, TEntity> : IRequestHandler<TCommand, ResultModel<object>>
  where TEntity : class, IEntity<object>
  where TCommand : UpdateCommandBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRepository<TEntity> _repository;
  private readonly IMapper _mapper;

  public UpdateCommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _repository = _unitOfWork.Repository<TEntity>();
    _mapper = mapper;
  }
  public async Task<ResultModel<object>> Handle(TCommand request, CancellationToken cancellationToken)
  {
    var query = _repository.SingleResultQuery()
                               .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                               .AndFilter(m => m.Id.Equals(request.Id));

    var entity = await _repository.FirstOrDefaultAsync(query, cancellationToken)
                 ?? throw new NotFoundException(typeof(TEntity).Name, request.Id);

    _mapper.Map(request, entity);

    await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)
                     .ConfigureAwait(false);

    return ResultModel<object>.Create(null);
  }
}
