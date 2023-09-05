using AutoMapper;
using Profio.Domain.Entities;

namespace Profio.Application.Orders;

public class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<Order, OrderDto>().ReverseMap();
  }
}
