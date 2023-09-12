using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Application.Deliveries;
using Profio.Application.Orders.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Orders.Queries;
public record GetDeliveryByOrderIdWithPagingQuery(string OrderId, Criteria Criteria) : GetWithPagingQueryBase<DeliveryDto>(Criteria);
public class GetDeliveryByOrderIdWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryByOrderIdWithPagingQuery, DeliveryDto, Delivery>
{
  public GetDeliveryByOrderIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Delivery, bool>> RequestFilter(GetDeliveryByOrderIdWithPagingQuery request)
  {
    return x => x.OrderId == request.OrderId;
  }
}
public class GetDeliveryByOrderIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetDeliveryByOrderIdWithPagingQuery, DeliveryDto>
{
  public GetDeliveryByOrderIdWithPagingQueryValidator(OrderExistenceByIdValidator orderValidator)
  {
    RuleFor(x => x.OrderId)
      .SetValidator(orderValidator);
  }
}
