using AutoMapper;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<ApplicationUser, UserDTO>().ReverseMap();
    CreateMap<ApplicationUser, AccountDto>().ReverseMap();
  }
}
