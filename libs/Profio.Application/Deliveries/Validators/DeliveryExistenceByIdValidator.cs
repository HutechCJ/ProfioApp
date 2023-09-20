using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Deliveries.Validators;

public sealed class DeliveryExistenceByIdValidator : EntityExistenceByIdValidator<Delivery>
{
  public DeliveryExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
