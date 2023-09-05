using AutoMapper;
using Profio.Application.Vehicles.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles;

public class VehicleProfile : Profile
{
  public VehicleProfile()
  {
    CreateMap<CreateVehicleCommand, Vehicle>().ReverseMap();
    CreateMap<Vehicle, VehicleDto>().ReverseMap();
    CreateMap<UpdateVehicleCommand, Vehicle>().ReverseMap();
  }
}
