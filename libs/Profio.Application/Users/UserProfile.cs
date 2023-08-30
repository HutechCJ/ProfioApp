using AutoMapper;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<ApplicationUser, UserDto>().ReverseMap();
    CreateMap<ApplicationUser, AccountDto>().ReverseMap();
  }
}
