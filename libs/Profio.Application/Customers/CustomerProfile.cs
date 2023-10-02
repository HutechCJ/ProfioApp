using Profio.Application.Customers.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Customers;

public sealed class
  CustomerProfile : EntityProfileBase<Customer, CustomerDto, CreateCustomerCommand, UpdateCustomerCommand>
{
}
