using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Orders.Queries;

public record GetOrderWithPagingQuery(Criteria<Order> Criteria) : GetWithPagingQueryBase<Order, OrderDto>(Criteria);

public class GetOrderWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderWithPagingQuery, OrderDto, Order>
{
  public GetOrderWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class
  GetOrderWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Order, GetOrderWithPagingQuery, OrderDto>
{
}
