using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Staffs.Queries;

public record GetStaffWithPagingQuery(Criteria<Staff> Criteria) : GetWithPagingQueryBase<Staff, StaffDto>(Criteria);

public class GetStaffWithPagingQueryHandler : GetWithPagingQueryHandler<GetStaffWithPagingQuery, StaffDto, Staff>
{
  public GetStaffWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  protected override Expression<Func<Staff, bool>> Filter(string filter)
  {
    return s => s.Name.ToLower().Contains(filter) || s.Phone != null && s.Phone.ToLower().Contains(filter);
  }
}

public class
  GetStaffWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Staff, GetStaffWithPagingQuery, StaffDto>
{
}
