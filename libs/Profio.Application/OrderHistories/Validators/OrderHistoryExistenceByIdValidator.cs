using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.OrderHistories.Validators;

public class OrderHistoryExistenceByIdValidator : EntityExistenceByIdValidator<OrderHistory>
{
  public OrderHistoryExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
