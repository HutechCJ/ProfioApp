using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Orders.Queries;

public sealed record GetOrderByIdQuery(object Id) : GetByIdQueryBase<OrderDto>(Id);

public sealed class GetOrderByIdQueryHandler : GetByIdQueryHandlerBase<GetOrderByIdQuery, OrderDto, Order>
{
  public GetOrderByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetOrderByIdQueryValidator : GetByIdQueryValidatorBase<GetOrderByIdQuery, OrderDto>
{
}
