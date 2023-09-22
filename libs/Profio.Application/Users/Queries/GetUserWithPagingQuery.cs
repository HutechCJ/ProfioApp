using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Identity;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;

namespace Profio.Application.Users.Queries;

public sealed record GetUserWithPagingQuery
  (Criteria Criteria) : GetWithPagingQueryBase<UserDto>(Criteria);

public sealed class GetUserWithPagingQueryHandler : GetWithPagingQueryHandler<GetUserWithPagingQuery, UserDto, ApplicationUser>
{
  public GetUserWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
