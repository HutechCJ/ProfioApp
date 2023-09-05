using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;

namespace Profio.Application.Customers.Queries;

public record GetCustomerWithPagingQuery
  (Criteria<Customer> Criteria) : GetWithPagingQueryBase<Customer, CustomerDto>(Criteria);

public class
  GetCustomerWithPagingQueryHandler : GetWithPagingQueryHandler<GetCustomerWithPagingQuery, CustomerDto, Customer>
{
  public GetCustomerWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class
  GetCustomerWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Customer, GetCustomerWithPagingQuery,
    CustomerDto>
{
}
