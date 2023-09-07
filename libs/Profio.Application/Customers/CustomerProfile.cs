using Profio.Application.CQRS;
using Profio.Application.Customers.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Customers;

public class CustomerProfile : EntityProfileBase<Customer, CustomerDto, CreateCustomerCommand, UpdateCustomerCommand> { }
