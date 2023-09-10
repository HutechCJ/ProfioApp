using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Orders.Queries;

public record GetOrderWithPagingQuery(Criteria Criteria) : GetWithPagingQueryBase<OrderDto>(Criteria);

public class GetOrderWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderWithPagingQuery, OrderDto, Order>
{
  public GetOrderWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class
  GetOrderWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetOrderWithPagingQuery, OrderDto>
{
}
