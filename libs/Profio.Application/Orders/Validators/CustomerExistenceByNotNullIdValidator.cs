using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Orders.Validators;

public class CustomerExistenceByNotNullIdValidator : EntityExistenceByIdValidator<Customer>
{
  public CustomerExistenceByNotNullIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
