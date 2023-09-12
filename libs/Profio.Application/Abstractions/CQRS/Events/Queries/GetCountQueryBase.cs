using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Interfaces;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Abstractions.CQRS.Events.Queries;

public record GetCountQueryBase : IRequest<int>;
public class GetCountQueryHandlerBase<TQuery, TEntity> : IRequestHandler<TQuery, int>
  where TQuery : GetCountQueryBase
  where TEntity : class, IEntity<object>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public GetCountQueryHandlerBase(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;
  public Task<int> Handle(TQuery request, CancellationToken cancellationToken)
  {
    return _applicationDbContext.Set<TEntity>().CountAsync(cancellationToken);
  }
}
