using AutoMapper;
using Profio.Domain.Entities;

namespace Profio.Application.Routes;

public class RouteProfile : Profile
{
  public RouteProfile()
  {
    CreateMap<Route, RouteDTO>().ReverseMap();
  }
}
