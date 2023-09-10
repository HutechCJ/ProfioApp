using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users.Queries;

public record GetUserWithPagingQuery
  (Criteria Criteria) : GetWithPagingQueryBase<UserDto>(Criteria);

public class GetUserWithPagingQueryHandler : GetWithPagingQueryHandler<GetUserWithPagingQuery, UserDto, ApplicationUser>
{
  public GetUserWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
