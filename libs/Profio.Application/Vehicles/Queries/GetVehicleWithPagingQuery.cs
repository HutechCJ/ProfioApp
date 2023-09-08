using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Vehicles.Queries;

public record GetVehicleWithPagingQuery
  (Criteria<Vehicle> Criteria) : GetWithPagingQueryBase<Vehicle, VehicleDto>(Criteria);

public class
  GetVehicleWithPagingQueryHandler : GetWithPagingQueryHandler<GetVehicleWithPagingQuery, VehicleDto, Vehicle>
{
  public GetVehicleWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class
  GetVehicleWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Vehicle, GetVehicleWithPagingQuery, VehicleDto>
{
}
