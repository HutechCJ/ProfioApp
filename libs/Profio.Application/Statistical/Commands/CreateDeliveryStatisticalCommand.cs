using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Views;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Persistence;

namespace Profio.Application.Statistical.Commands;

public sealed record CreateDeliveryStatisticalCommand(object Id) : GetByIdQueryBase<DeliverySummary>(Id);

public sealed class CreateDeliveryStatisticalCommandHandler : IRequestHandler<CreateDeliveryStatisticalCommand, DeliverySummary>
{
  private readonly ApplicationDbContext _dbContext;
  private readonly IRepository<DeliverySummary> _repository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateDeliveryStatisticalCommandHandler(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
    => (_unitOfWork, _dbContext, _repository) = (unitOfWork, dbContext, unitOfWork.Repository<DeliverySummary>());

  public async Task<DeliverySummary> Handle(CreateDeliveryStatisticalCommand request, CancellationToken cancellationToken)
  {
    var deliverySummaryData = from delivery in _dbContext.Deliveries
                              join order in _dbContext.Orders on delivery.OrderId equals order.Id
                              where ReferenceEquals(delivery.Id, request.Id)
                              select new
                              {
                                OrderId = order.Id,
                                order.Status,
                                order.Distance
                              };

    var deliverySummaries = await deliverySummaryData.ToListAsync(cancellationToken);

    var deliverySummary = deliverySummaries.GroupBy(x => x.OrderId)
      .Select(x => new DeliverySummary
        (
          Id: Guid.NewGuid(),
          DeliveryId: request.Id.ToString(),
          TotalOrder: x.Count(),
          Orders: x.Select(arg => new DeliverySummary.DeliveryOrder
          (
            OrderId: arg.OrderId,
            Status: arg.Status,
            Distance: arg.Distance
          )).ToList()
        )
      )
      .First();

    await _repository.AddAsync(deliverySummary, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

    _repository.RemoveTracking(deliverySummary);

    return deliverySummary;
  }
}
