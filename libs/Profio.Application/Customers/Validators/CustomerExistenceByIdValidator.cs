using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Customers.Validators;

public class CustomerExistenceByIdValidator : EntityExistenceByIdValidator<Customer>
{
  public CustomerExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
