using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Constants;
using Profio.Domain.Contracts;
using Profio.Domain.Entities;
using System.Net.Http.Json;

namespace Profio.Infrastructure.Persistence.Triggers;
public sealed class AfterUpdateOrderTrigger : IAfterSaveTrigger<Order>
{
  private readonly ApplicationDbContext _applicationDbContext;
  private readonly IHttpClientFactory _httpClientFactory;

  public AfterUpdateOrderTrigger(ApplicationDbContext applicationDbContext, IHttpClientFactory httpClientFactory) => (_applicationDbContext, _httpClientFactory) = (applicationDbContext, httpClientFactory);

  public async Task AfterSave(ITriggerContext<Order> context, CancellationToken cancellationToken)
  {
    if (context.UnmodifiedEntity is null) return;

    var unmodifiedOrder = await _applicationDbContext.Orders
      .SingleOrDefaultAsync(x => x.Id == context.UnmodifiedEntity.Id, cancellationToken);

    var modifiedOrder = await _applicationDbContext.Orders
      .Include(x => x.Customer)
      .SingleOrDefaultAsync(x => x.Id == context.Entity.Id, cancellationToken);

    if (unmodifiedOrder is null || modifiedOrder is null) return;
    unmodifiedOrder.Status = context.UnmodifiedEntity.Status;

    if (!IsValid(context, unmodifiedOrder, modifiedOrder)) return;

    var client = _httpClientFactory.CreateClient("Api");

    var orderInfo = new OrderInfo
    {
      Id = modifiedOrder.Id,
      CustomerName = modifiedOrder.Customer?.Name,
      Email = modifiedOrder.Customer?.Email,
      From = modifiedOrder.Customer?.Address?.Province,
      To = modifiedOrder.DestinationAddress?.Province,
      Phone = modifiedOrder.Customer?.Phone,
    };

    await client.PostAsJsonAsync(
      $"sender/email/order?type={GetEmailType(context.Entity.Status)}",
      orderInfo,
      cancellationToken: cancellationToken);
  }

  private static bool IsValid(ITriggerContext<Order> context, Order unmodified, Order modified)
    => context.ChangeType == ChangeType.Modified
       && unmodified.Status != modified.Status
       && modified is { Customer:
         { Phone.Length: 10, Name.Length: > 0, Email.Length: > 0, Address.Province: { }
         },
         DestinationAddress.Province: { }
       };


  private static EmailType GetEmailType(OrderStatus status)
    => status switch
    {
      OrderStatus.Pending => EmailType.OrderPending,
      OrderStatus.InProgress => EmailType.OrderInProcess,
      OrderStatus.Completed => EmailType.OrderCompleted,
      OrderStatus.Received => EmailType.OrderArrived,
      OrderStatus.Cancelled => EmailType.CancelOrder,
      _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid order status!"),
    };
}
