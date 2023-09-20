using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;
using System.Linq.Expressions;

namespace Profio.Application.Staffs.Queries;

public sealed record GetStaffWithPagingQuery(Criteria Criteria, StaffEnumFilter StaffEnumFilter) : GetWithPagingQueryBase<StaffDto>(Criteria);

public sealed class GetStaffWithPagingQueryHandler : GetWithPagingQueryHandler<GetStaffWithPagingQuery, StaffDto, Staff>
{
  public GetStaffWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Staff, bool>> Filter(string filter)
  {
    return s => s.Name.ToLower().Contains(filter)
      || (s.Phone != null && s.Phone.ToLower().Contains(filter));
  }
  protected override Expression<Func<Staff, bool>> RequestFilter(GetStaffWithPagingQuery request)
  {
    return x => request.StaffEnumFilter.Position == null || x.Position == request.StaffEnumFilter.Position;
  }
}

public sealed class
  GetStaffWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetStaffWithPagingQuery, StaffDto>
{
}
