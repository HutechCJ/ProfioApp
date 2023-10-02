using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Deliveries.Validators;
using Profio.Application.OrderHistories;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Deliveries.Queries;

public sealed record GetOrderHistoryByDeliveryIdWithPagingQuery
  (string DeliveryId, Criteria Criteria) : GetWithPagingQueryBase<OrderHistoryDto>(Criteria);

public sealed class GetOrderHistoryByDeliveryIdWithPagingQueryHandler : GetWithPagingQueryHandler<
  GetOrderHistoryByDeliveryIdWithPagingQuery, OrderHistoryDto, OrderHistory>
{
  public GetOrderHistoryByDeliveryIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(
    unitOfWork, mapper)
  {
  }

  protected override Expression<Func<OrderHistory, bool>> RequestFilter(
    GetOrderHistoryByDeliveryIdWithPagingQuery request)
  {
    return x => x.DeliveryId == request.DeliveryId;
  }
}

public sealed class GetOrderHistoryByDeliveryIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<
  GetOrderHistoryByDeliveryIdWithPagingQuery, OrderHistoryDto>
{
  public GetOrderHistoryByDeliveryIdWithPagingQueryValidator(DeliveryExistenceByIdValidator deliveryValidator)
    => RuleFor(x => x.DeliveryId).SetValidator(deliveryValidator);
}
