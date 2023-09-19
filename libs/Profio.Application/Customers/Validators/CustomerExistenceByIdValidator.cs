using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Customers.Validators;

public sealed class CustomerExistenceByIdValidator : EntityExistenceByIdValidator<Customer>
{
  public CustomerExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
