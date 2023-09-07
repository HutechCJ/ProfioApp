using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Vehicles.Validators;

public class VehicleExistenceByIdValidator : EntityExistenceByIdValidator<Vehicle>
{
  public VehicleExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
