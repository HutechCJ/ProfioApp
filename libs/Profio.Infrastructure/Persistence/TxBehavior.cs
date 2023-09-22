using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Interfaces;
using System.Data;

namespace Profio.Infrastructure.Persistence;

public sealed class TxBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : notnull
{
  private readonly IDatabaseFacade _databaseFacade;

  public TxBehavior(IDatabaseFacade databaseFacade)
    => _databaseFacade = databaseFacade;

  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  {
    if (request is not ITxRequest)
      return await next();

    var strategy = _databaseFacade.Database.CreateExecutionStrategy();

    return await strategy.ExecuteAsync(async () =>
    {
      await using var transaction = await _databaseFacade.Database
        .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
      var response = await next();
      await transaction.CommitAsync(cancellationToken);
      return response;
    }).ConfigureAwait(false);
  }
}
