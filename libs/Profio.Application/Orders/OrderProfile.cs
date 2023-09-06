using AutoMapper;
using Profio.Application.Orders.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Orders;

public class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<Order, OrderDto>().ReverseMap();
    CreateMap<CreateOrderCommand, Order>().ReverseMap();
    CreateMap<UpdateOrderCommand, Order>().ReverseMap();
  }
}
