using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs.Validators;

public class HubExistenceByIdValidator : EntityExistenceByIdValidator<Hub>
{
  public HubExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
