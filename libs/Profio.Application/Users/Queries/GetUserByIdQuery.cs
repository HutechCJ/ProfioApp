//using AutoMapper;
//using Profio.Domain.Interfaces;
//using Profio.Infrastructure.CQRS.Events.Queries;
//using Profio.Infrastructure.CQRS.Handlers.Queries;
//using Profio.Infrastructure.Identity;

//namespace Profio.Application.Users.Queries;

//public record GetUserByIdQuery(object Id) : GetQueryByIdBase<UserDTO>(Id);
//public class GetUserByIdQueryHandler : GetByIdQueryHandlerBase<GetUserByIdQuery, UserDTO, ApplicationUser>
//{
//  public GetUserByIdQueryHandler(IUnitOfWork<ApplicationUser> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
//  {
//  }
//}
