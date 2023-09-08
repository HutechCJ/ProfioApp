using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Queries;
using Profio.Application.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users.Queries;

public record GetUserByIdQuery(object Id) : GetByIdQueryBase<UserDto>(Id);

public class GetUserByIdQueryHandler : GetByIdQueryHandlerBase<GetUserByIdQuery, UserDto, ApplicationUser>
{
  public GetUserByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
