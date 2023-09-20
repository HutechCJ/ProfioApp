using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Phases.Validators;

public sealed class PhaseExistenceByIdValidator : EntityExistenceByIdValidator<Phase>
{
  public PhaseExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
