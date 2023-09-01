using AutoMapper;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs;

public class HubProfile : Profile
{
  public HubProfile()
  {
    CreateMap<Hub, HubDTO>().ReverseMap();
  }
}
