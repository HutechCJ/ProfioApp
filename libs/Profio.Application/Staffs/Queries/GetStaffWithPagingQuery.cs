using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Staffs.Queries;

public record GetStaffWithPagingQuery(Criteria Criteria, StaffEnumFilter StaffEnumFilter) : GetWithPagingQueryBase<StaffDto>(Criteria);

public class GetStaffWithPagingQueryHandler : GetWithPagingQueryHandler<GetStaffWithPagingQuery, StaffDto, Staff>
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

public class
  GetStaffWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetStaffWithPagingQuery, StaffDto>
{
}
