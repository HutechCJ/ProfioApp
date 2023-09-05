using AutoMapper;
using Profio.Application.Hubs.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs;

public class HubProfile : Profile
{
  public HubProfile()
  {
    CreateMap<Hub, HubDto>().ReverseMap();
     CreateMap<CreateHubCommand, Hub>().ReverseMap();
    CreateMap<UpdateHubCommand, Hub>().ReverseMap();
  }
}
