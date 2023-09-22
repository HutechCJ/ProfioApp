using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.OrderHistories.Queries;

public sealed record GetOrderHistoryByIdQuery(object Id) : GetByIdQueryBase<OrderHistoryDto>(Id);

public sealed class GetOrderHistoryByIdQueryHandler : GetByIdQueryHandlerBase<GetOrderHistoryByIdQuery, OrderHistoryDto, OrderHistory>
{
  public GetOrderHistoryByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetOrderHistoryByIdQueryValidator : GetByIdQueryValidatorBase<GetOrderHistoryByIdQuery, OrderHistoryDto>
{
}
