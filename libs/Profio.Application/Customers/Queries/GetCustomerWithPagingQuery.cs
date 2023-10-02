using System.Linq.Expressions;
using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Domain.Specifications;
using Profio.Infrastructure.Abstractions.CQRS.Events.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Queries;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Customers.Queries;

public sealed record GetCustomerWithPagingQuery
  (Specification Specification, CustomerEnumFilter CustomerEnumFilter) : GetWithPagingQueryBase<CustomerDto>(
    Specification);

public sealed class
  GetCustomerWithPagingQueryHandler : GetWithPagingQueryHandler<GetCustomerWithPagingQuery, CustomerDto, Customer>
{
  public GetCustomerWithPagingQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  protected override Expression<Func<Customer, bool>> Filter(string filter)
    => c
      => (c.Name != null && c.Name.ToLower().Contains(filter))
         || (c.Email != null && c.Email.ToLower().Contains(filter))
         || (c.Address != null && ((c.Address.Street != null
                                    && c.Address.Street.ToLower().Contains(filter))
                                   || (c.Address.Province != null
                                       && c.Address.Province.ToLower().Contains(filter))
                                   || (c.Address.Ward != null
                                       && c.Address.Ward.ToLower().Contains(filter))
                                   || (c.Address.City != null
                                       && c.Address.City.ToLower().Contains(filter))
                                   || (c.Address.ZipCode != null
                                       && c.Address.ZipCode.ToLower().Contains(filter))
           ));

  protected override Expression<Func<Customer, bool>> RequestFilter(GetCustomerWithPagingQuery request)
    => x => request.CustomerEnumFilter.Gender == null || x.Gender == request.CustomerEnumFilter.Gender;
}

public sealed class
  GetCustomerWithPagingQueryValidator : GetWithPagingQueryValidatorBase<GetCustomerWithPagingQuery,
    CustomerDto>
{
}
