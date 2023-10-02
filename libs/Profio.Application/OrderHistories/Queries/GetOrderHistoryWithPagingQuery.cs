using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.OrderHistories.Queries;

public sealed record GetOrderHistoryWithPagingQuery
  (Specification Specification) : GetWithPagingQueryBase<OrderHistoryDto>(Specification);

public sealed class
  GetOrderHistoryWithPagingQueryHandler : GetWithPagingQueryHandler<GetOrderHistoryWithPagingQuery, OrderHistoryDto,
    OrderHistory>
{
  public GetOrderHistoryWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class
  GetOrderHistoryWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetOrderHistoryWithPagingQuery,
    OrderHistoryDto>
{
}
