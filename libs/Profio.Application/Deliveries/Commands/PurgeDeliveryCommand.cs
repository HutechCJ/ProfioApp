using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Deliveries.Commands;

public sealed record PurgeDeliveryCommand : IRequest<Unit>;
public sealed class PurgeDeliveryCommandHanlder : IRequestHandler<PurgeDeliveryCommand, Unit>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public PurgeDeliveryCommandHanlder(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;
  public async Task<Unit> Handle(PurgeDeliveryCommand request, CancellationToken cancellationToken)
  {
    await _applicationDbContext.Deliveries
      .ExecuteDeleteAsync(cancellationToken);
    return Unit.Value;
  }
}
