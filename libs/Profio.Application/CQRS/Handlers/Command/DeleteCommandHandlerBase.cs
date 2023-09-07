using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Application.CQRS.Events.Commands;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;
using Profio.Infrastructure.Exceptions;

namespace Profio.Application.CQRS.Handlers.Command;

public class DeleteCommandHandlerBase<TCommand, TModel, TEntity> : IRequestHandler<TCommand, TModel>
  where TCommand : DeleteCommandBase<TModel>
  where TEntity : class, IEntity<object>
  where TModel : BaseModel
{
  private readonly IMapper _mapper;
  private readonly IRepository<TEntity> _repository;

  public DeleteCommandHandlerBase(IRepositoryFactory unitOfWork, IMapper mapper)
    => (_repository, _mapper) = (unitOfWork.Repository<TEntity>(), mapper);

  public async Task<TModel> Handle(TCommand request, CancellationToken cancellationToken)
  {
    var query = _repository.SingleResultQuery()
      .AndFilter(m => m.Id.Equals(request.Id));

    var model = await _repository
      .ToQueryable(query)
      .ProjectTo<TModel>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(TEntity).Name, request.Id);

    await _repository.RemoveAsync(x => x.Id.Equals(request.Id), cancellationToken).ConfigureAwait(false);

    return model;
  }
}
