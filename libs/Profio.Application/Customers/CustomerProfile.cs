using AutoMapper;
using Profio.Application.Customers.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Customers;

public class CustomerProfile : Profile
{
  public CustomerProfile()
  {
    CreateMap<CreateCustomerCommand, Customer>();
    CreateMap<Customer, CustomerDto>();
  }
}
