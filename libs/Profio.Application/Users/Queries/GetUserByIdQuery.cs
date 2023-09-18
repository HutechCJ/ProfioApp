using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Domain.Identity;

namespace Profio.Application.Users.Queries;

public sealed record GetUserByIdQuery(object Id) : GetByIdQueryBase<UserDto>(Id);

public sealed class GetUserByIdQueryHandler : GetByIdQueryHandlerBase<GetUserByIdQuery, UserDto, ApplicationUser>
{
  public GetUserByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
