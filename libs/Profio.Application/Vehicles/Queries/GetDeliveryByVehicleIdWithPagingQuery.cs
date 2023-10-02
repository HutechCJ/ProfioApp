using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Deliveries;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetDeliveryByVehicleIdWithPagingQuery
  (string VehicleId, Criteria Criteria) : GetWithPagingQueryBase<DeliveryDto>(Criteria);

public sealed class
  GetDeliveryByVehicleIdWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryByVehicleIdWithPagingQuery,
    DeliveryDto, Delivery>
{
  public GetDeliveryByVehicleIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork,
    mapper)
  {
  }

  protected override Expression<Func<Delivery, bool>> RequestFilter(GetDeliveryByVehicleIdWithPagingQuery request)
    => x => x.VehicleId == request.VehicleId;
}

public sealed class
  GetDeliveryByVehicleIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetDeliveryByVehicleIdWithPagingQuery
    , DeliveryDto>
{
  public GetDeliveryByVehicleIdWithPagingQueryValidator(VehicleExistenceByIdValidator vehicleValidator)
    => RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);
}
