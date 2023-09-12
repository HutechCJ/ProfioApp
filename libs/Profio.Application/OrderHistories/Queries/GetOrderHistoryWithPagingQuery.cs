using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.OrderHistories.Queries;

public record GetOrderHistoryWithPagingQuery(Criteria Criteria) : GetWithPagingQueryBase<OrderHistoryDto>(Criteria);

public class GetOrderHistoryWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderHistoryWithPagingQuery, OrderHistoryDto, OrderHistory>
{
  public GetOrderHistoryWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class GetOrderHistoryWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetOrderHistoryWithPagingQuery, OrderHistoryDto>
{
}
