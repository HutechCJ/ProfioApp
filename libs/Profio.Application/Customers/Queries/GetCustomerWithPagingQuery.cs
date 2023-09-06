using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using System.Linq.Expressions;

namespace Profio.Application.Customers.Queries;

public record GetCustomerWithPagingQuery
  (Criteria<Customer> Criteria) : GetWithPagingQueryBase<Customer, CustomerDto>(Criteria);

public class
  GetCustomerWithPagingQueryHandler : GetWithPagingQueryHandler<GetCustomerWithPagingQuery, CustomerDto, Customer>
{

  public GetCustomerWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
  protected override Expression<Func<Customer, bool>> Filter(string filter)
    => c
      => c.Name!.ToLower().Contains(filter)
      || c.Email!.ToLower().Contains(filter)
      || (c.Address != null && ((c.Address.Street != null
        && c.Address.Street.ToLower().Contains(filter))
      || (c.Address.Province != null
        && c.Address.Province.ToLower().Contains(filter))
      || (c.Address.Ward != null
        && c.Address.Ward.ToLower().Contains(filter))
      || (c.Address.City != null
        && c.Address.City.ToLower().Contains(filter))));
}

public class
  GetCustomerWithPagingQueryValidator : GetWithPagingQueryValidatorBase<Customer, GetCustomerWithPagingQuery,
    CustomerDto>
{
}
