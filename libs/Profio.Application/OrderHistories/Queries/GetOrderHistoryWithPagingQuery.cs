using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.OrderHistories.Queries;

public record GetOrderHistoryWithPagingQuery(Criteria<OrderHistory> Criteria) : GetWithPagingQueryBase<OrderHistory, OrderHistoryDto>(Criteria);

public class GetOrderHistoryWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderHistoryWithPagingQuery, OrderHistoryDto, OrderHistory>
{
  public GetOrderHistoryWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class GetOrderHistoryWithPagingQueryValidator : GetWithPagingQueryValidatorBase<OrderHistory, GetOrderHistoryWithPagingQuery, OrderHistoryDto>
{
}
