using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.OrderHistories.Validators;

public sealed class OrderHistoryExistenceByIdValidator : EntityExistenceByIdValidator<OrderHistory>
{
  public OrderHistoryExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
