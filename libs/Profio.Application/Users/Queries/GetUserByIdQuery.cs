using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Identity;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;

namespace Profio.Application.Users.Queries;

public sealed record GetUserByIdQuery(object Id) : GetByIdQueryBase<UserDto>(Id);

public sealed class GetUserByIdQueryHandler : GetByIdQueryHandlerBase<GetUserByIdQuery, UserDto, ApplicationUser>
{
  public GetUserByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
