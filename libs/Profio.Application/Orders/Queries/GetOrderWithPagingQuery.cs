using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Orders.Queries;

public record GetOrderWithPagingQuery(Criteria Criteria, OrderEnumFilter OrderEnumFilter) : GetWithPagingQueryBase<OrderDto>(Criteria);

public class GetOrderWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderWithPagingQuery, OrderDto, Order>
{
  public GetOrderWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Order, bool>> RequestFilter(GetOrderWithPagingQuery request)
  {
    return x => x.Status == request.OrderEnumFilter.Status;
  }
}

public class
  GetOrderWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetOrderWithPagingQuery, OrderDto>
{
}
