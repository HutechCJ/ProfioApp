using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Orders.Queries;

public record GetOrderByIdQuery(object Id) : GetByIdQueryBase<OrderDto>(Id);

public class GetOrderByIdQueryHandler : GetByIdQueryHandlerBase<GetOrderByIdQuery, OrderDto, Order>
{
  public GetOrderByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class GetOrderByIdQueryValidator : GetByIdQueryValidatorBase<GetOrderByIdQuery, OrderDto>
{
}
