using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Validators;

public class StaffExistenceByNotNullIdValidator : EntityExistenceByIdValidator<Staff>
{
  public StaffExistenceByNotNullIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
