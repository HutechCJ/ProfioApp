using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Orders;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetCurrentOrderByVehicleIdWithPagingQuery
  (string VehicleId, Specification Specification) : GetWithPagingQueryBase<OrderDto>(Specification);

public sealed class
  GetCurrentOrderByVehicleIdWithPagingQueryHandler : GetWithPagingQueryHandler<GetCurrentOrderByVehicleIdWithPagingQuery
    , OrderDto, Order>
{
  public GetCurrentOrderByVehicleIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(
    unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Order, bool>> RequestFilter(GetCurrentOrderByVehicleIdWithPagingQuery request)
    => x => x.Deliveries != null && x.Deliveries.Any(d => d.VehicleId == request.VehicleId) &&
            x.Status == OrderStatus.InProgress;
}

public sealed class
  GetCurrentOrderByVehicleIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<
    GetCurrentOrderByVehicleIdWithPagingQuery, OrderDto>
{
  public GetCurrentOrderByVehicleIdWithPagingQueryValidator(VehicleExistenceByIdValidator vehicleValidator)
    => RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);
}
