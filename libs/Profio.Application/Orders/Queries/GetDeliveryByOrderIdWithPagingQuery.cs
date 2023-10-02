using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Deliveries;
using Profio.Application.Orders.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Orders.Queries;

public sealed record GetDeliveryByOrderIdWithPagingQuery
  (string OrderId, Criteria Criteria) : GetWithPagingQueryBase<DeliveryDto>(Criteria);

public sealed class
  GetDeliveryByOrderIdWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryByOrderIdWithPagingQuery,
    DeliveryDto, Delivery>
{
  public GetDeliveryByOrderIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork,
    mapper)
  {
  }

  protected override Expression<Func<Delivery, bool>> RequestFilter(GetDeliveryByOrderIdWithPagingQuery request)
  {
    return x => x.OrderId == request.OrderId;
  }
}

public sealed class
  GetDeliveryByOrderIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetDeliveryByOrderIdWithPagingQuery,
    DeliveryDto>
{
  public GetDeliveryByOrderIdWithPagingQueryValidator(OrderExistenceByIdValidator orderValidator)
    => RuleFor(x => x.OrderId)
      .SetValidator(orderValidator);
}
