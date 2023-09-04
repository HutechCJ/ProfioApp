using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Domain.Entities;

namespace Profio.Application.Customers.Queries;

public record GetCustomerByIdQuery(object Id) : GetByIdQueryBase<CustomerDto>(Id);

public class GetCustomerByIdQueryHandler : GetByIdQueryHandlerBase<GetCustomerByIdQuery, CustomerDto, Customer>
{
  public GetCustomerByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
