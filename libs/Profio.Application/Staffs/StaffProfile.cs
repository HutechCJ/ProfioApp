using AutoMapper;
using Profio.Application.Staffs.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Staffs;

public class StaffProfile : Profile
{
  public StaffProfile()
  {
    CreateMap<Staff, StaffDTO>().ReverseMap();
    CreateMap<CreateStaffCommand, Staff>();
  }
}
