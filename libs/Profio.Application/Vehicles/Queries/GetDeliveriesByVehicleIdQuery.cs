using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Deliveries;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Vehicles.Queries;

public record GetDeliveriesByVehicleIdQuery(string VehicleId, Criteria Criteria) : GetWithPagingQueryBase<DeliveryDto>(Criteria);
public class GetDeliveriesByVehicleIdQueryHandler : GetWithPagingQueryHandler<GetDeliveriesByVehicleIdQuery, DeliveryDto, Delivery>
{
  public GetDeliveriesByVehicleIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Delivery, bool>> RequestFilter(GetDeliveriesByVehicleIdQuery request)
  {
    return x => x.VehicleId == request.VehicleId;
  }
}
public class GetDeliveriesByVehicleIdQueryValidator : AbstractValidator<GetDeliveriesByVehicleIdQuery>
{
  public GetDeliveriesByVehicleIdQueryValidator(VehicleExistenceByIdValidator vehicleValidator)
  {
    RuleFor(x => x.VehicleId)
      .SetValidator(vehicleValidator);
  }
}
