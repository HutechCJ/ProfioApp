using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Application.Deliveries.Validators;
using Profio.Application.OrderHistories;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Deliveries.Queries;
public record GetOrderHistoryByDeliveryIdWithPagingQuery(string DeliveryId, Criteria Criteria) : GetWithPagingQueryBase<OrderHistoryDto>(Criteria);
public class GetOrderHistoryByDeliveryIdWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderHistoryByDeliveryIdWithPagingQuery, OrderHistoryDto, OrderHistory>
{
  public GetOrderHistoryByDeliveryIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<OrderHistory, bool>> RequestFilter(GetOrderHistoryByDeliveryIdWithPagingQuery request)
  {
    return x => x.DeliveryId == request.DeliveryId;
  }
}
public class GetOrderHistoryByDeliveryIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetOrderHistoryByDeliveryIdWithPagingQuery, OrderHistoryDto>
{
  public GetOrderHistoryByDeliveryIdWithPagingQueryValidator(DeliveryExistenceByIdValidator deliveryValidator)
  {
    RuleFor(x => x.DeliveryId)
      .SetValidator(deliveryValidator);
  }
}

