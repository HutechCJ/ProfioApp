using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users.Queries;

public record GetUserWithPagingQuery(Criteria<ApplicationUser> Criteria) : GetWithPagingQueryBase<ApplicationUser, UserDto>(Criteria);
public class GetUserWithPagingQueryHandler : GetWithPagingQueryHandler<GetUserWithPagingQuery, UserDto, ApplicationUser>
{
  public GetUserWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
