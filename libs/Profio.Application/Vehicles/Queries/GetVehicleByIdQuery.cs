using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Queries;

public record GetVehicleByIdQuery(object Id) : GetByIdQueryBase<VehicleDto>(Id);

public class GetVehicleByIdQueryHandler : GetByIdQueryHandlerBase<GetVehicleByIdQuery, VehicleDto, Vehicle>
{
    public GetVehicleByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
public class GetVehicleByIdQueryValidator : GetByIdQueryValidatorBase<GetVehicleByIdQuery, VehicleDto> { }
