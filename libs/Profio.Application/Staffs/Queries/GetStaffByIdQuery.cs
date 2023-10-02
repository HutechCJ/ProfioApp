using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Staffs.Queries;

public sealed record GetStaffByIdQuery(object Id) : GetByIdQueryBase<StaffDto>(Id);

public sealed class GetStaffByIdQueryHandler : GetByIdQueryHandlerBase<GetStaffByIdQuery, StaffDto, Staff>
{
  public GetStaffByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class GetStaffByIdQueryValidator : GetByIdQueryValidatorBase<GetStaffByIdQuery, StaffDto>
{
}
