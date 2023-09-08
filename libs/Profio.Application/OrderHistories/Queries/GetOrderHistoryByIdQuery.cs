using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.OrderHistories.Queries;

public record GetOrderHistoryByIdQuery(object Id) : GetByIdQueryBase<OrderHistoryDto>(Id);

public class GetOrderHistoryByIdQueryHandler : GetByIdQueryHandlerBase<GetOrderHistoryByIdQuery, OrderHistoryDto, OrderHistory>
{
  public GetOrderHistoryByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class GetOrderHistoryByIdQueryValidator : GetByIdQueryValidatorBase<GetOrderHistoryByIdQuery, OrderHistoryDto>
{
}
