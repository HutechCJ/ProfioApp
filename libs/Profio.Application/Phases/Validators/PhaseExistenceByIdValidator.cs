using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Phases.Validators;

public sealed class PhaseExistenceByIdValidator : EntityExistenceByIdValidator<Phase>
{
  public PhaseExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
