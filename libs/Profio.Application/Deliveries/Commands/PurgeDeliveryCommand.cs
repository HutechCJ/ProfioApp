using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Deliveries.Commands;

public sealed record PurgeDeliveryCommand : IRequest<Unit>;

public sealed class PurgeDeliveryCommandHandler : IRequestHandler<PurgeDeliveryCommand, Unit>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public PurgeDeliveryCommandHandler(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;

  public async Task<Unit> Handle(PurgeDeliveryCommand request, CancellationToken cancellationToken)
  {
    await _applicationDbContext.Deliveries
      .ExecuteDeleteAsync(cancellationToken);
    return Unit.Value;
  }
}
