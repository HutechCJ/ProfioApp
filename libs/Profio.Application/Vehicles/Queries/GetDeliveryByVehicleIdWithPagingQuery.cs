using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Application.Deliveries;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Vehicles.Queries;

public sealed record GetDeliveryByVehicleIdWithPagingQuery(string VehicleId, Criteria Criteria) : GetWithPagingQueryBase<DeliveryDto>(Criteria);
public sealed class GetDeliveryByVehicleIdWithPagingQueryHandler : GetWithPagingQueryHandler<GetDeliveryByVehicleIdWithPagingQuery, DeliveryDto, Delivery>
{
  public GetDeliveryByVehicleIdWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Delivery, bool>> RequestFilter(GetDeliveryByVehicleIdWithPagingQuery request)
  {
    return x => x.VehicleId == request.VehicleId;
  }
}
public sealed class GetDeliveryByVehicleIdWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetDeliveryByVehicleIdWithPagingQuery, DeliveryDto>
{
  public GetDeliveryByVehicleIdWithPagingQueryValidator(VehicleExistenceByIdValidator vehicleValidator)
  {
    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);
  }
}
