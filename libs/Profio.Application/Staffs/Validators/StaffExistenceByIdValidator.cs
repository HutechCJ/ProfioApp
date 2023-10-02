using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Staffs.Validators;

public sealed class StaffExistenceByIdValidator : EntityExistenceByIdValidator<Staff>
{
  public StaffExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
