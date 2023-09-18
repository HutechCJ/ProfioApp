using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Constants;
using Profio.Domain.Entities;

namespace Profio.Infrastructure.Persistence.Triggers;

public sealed class AfterCreateDeliveryTrigger : IAfterSaveTrigger<Delivery>
{
  private readonly ApplicationDbContext _applicationDbContext;

  public AfterCreateDeliveryTrigger(ApplicationDbContext applicationDbContext)
    => _applicationDbContext = applicationDbContext;
  public async Task AfterSave(ITriggerContext<Delivery> context, CancellationToken cancellationToken)
  {
    if (context.ChangeType == ChangeType.Added)
    {
      var order = await _applicationDbContext.Orders
        .SingleOrDefaultAsync(x => x.Id == context.Entity.OrderId, cancellationToken);
      if (order is null)
        return;
      var hub = await _applicationDbContext.Hubs
        .FirstOrDefaultAsync(x => x.ZipCode == order.DestinationZipCode, cancellationToken);
      if (hub is null) return;
      var orderHistory = new OrderHistory
      {
        Delivery = context.Entity,
        Hub = hub
      };
      order.Status = OrderStatus.InProgress;
      _applicationDbContext.Orders.Update(order);
      await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
  }
}
