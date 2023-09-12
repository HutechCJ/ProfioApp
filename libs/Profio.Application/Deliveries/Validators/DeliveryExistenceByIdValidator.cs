using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Deliveries.Validators;

public class DeliveryExistenceByIdValidator : EntityExistenceByIdValidator<Delivery>
{
  public DeliveryExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
