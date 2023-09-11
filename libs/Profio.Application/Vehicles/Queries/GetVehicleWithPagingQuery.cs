using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Vehicles.Queries;

public record GetVehicleWithPagingQuery
  (Criteria Criteria, VehicleEnumFilter VehicleEnumFilter) : GetWithPagingQueryBase<VehicleDto>(Criteria);

public class
  GetVehicleWithPagingQueryHandler : GetWithPagingQueryHandler<GetVehicleWithPagingQuery, VehicleDto, Vehicle>
{
  public GetVehicleWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Vehicle, bool>> RequestFilter(GetVehicleWithPagingQuery request)
  {
    return x => (request.VehicleEnumFilter.Status == null || x.Status == request.VehicleEnumFilter.Status)
    && (request.VehicleEnumFilter.Type == null || x.Type == request.VehicleEnumFilter.Type);
  }
}

public class
  GetVehicleWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetVehicleWithPagingQuery, VehicleDto>
{
}
