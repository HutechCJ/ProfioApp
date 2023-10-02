using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Staffs.Queries;

public sealed record GetStaffWithPagingQuery
  (Specification Specification, StaffEnumFilter StaffEnumFilter) : GetWithPagingQueryBase<StaffDto>(Specification);

public sealed class GetStaffWithPagingQueryHandler : GetWithPagingQueryHandler<GetStaffWithPagingQuery, StaffDto, Staff>
{
  public GetStaffWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Staff, bool>> Filter(string filter)
    => s => s.Name.ToLower().Contains(filter)
            || (s.Phone != null && s.Phone.ToLower().Contains(filter));

  protected override Expression<Func<Staff, bool>> RequestFilter(GetStaffWithPagingQuery request)
    => x => request.StaffEnumFilter.Position == null || x.Position == request.StaffEnumFilter.Position;
}

public sealed class
  GetStaffWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetStaffWithPagingQuery, StaffDto>
{
}
