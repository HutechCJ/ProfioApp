using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Validators;

public class StaffExistenceByIdValidator : EntityExistenceByIdValidator<Staff>
{
  public StaffExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
