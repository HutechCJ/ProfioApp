using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Staffs.Queries;

public record GetStaffByIdQuery(object Id) : GetByIdQueryBase<StaffDto>(Id);

public class GetStaffByIdQueryHandler : GetByIdQueryHandlerBase<GetStaffByIdQuery, StaffDto, Staff>
{
  public GetStaffByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
public class GetStaffByIdQueryValidator : GetByIdQueryValidatorBase<GetStaffByIdQuery, StaffDto> { }
