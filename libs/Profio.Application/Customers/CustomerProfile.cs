using Profio.Application.Abstractions.CQRS;
using Profio.Application.Customers.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Customers;

public sealed class CustomerProfile : EntityProfileBase<Customer, CustomerDto, CreateCustomerCommand, UpdateCustomerCommand> { }
