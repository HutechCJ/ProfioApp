using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.OrderHistories.Validators;

public sealed class OrderHistoryExistenceByIdValidator : EntityExistenceByIdValidator<OrderHistory>
{
  public OrderHistoryExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
