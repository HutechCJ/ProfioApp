using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Validators;

public sealed class VehicleExistenceByIdValidator : EntityExistenceByIdValidator<Vehicle>
{
  public VehicleExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
