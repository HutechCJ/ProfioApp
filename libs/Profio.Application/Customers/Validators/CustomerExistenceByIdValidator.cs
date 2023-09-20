using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Customers.Validators;

public sealed class CustomerExistenceByIdValidator : EntityExistenceByIdValidator<Customer>
{
  public CustomerExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
