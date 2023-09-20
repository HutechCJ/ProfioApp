using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Routes.Validators;

public sealed class RouteExistenceByIdValidator : EntityExistenceByIdValidator<Route>
{
  public RouteExistenceByIdValidator(IRepositoryFactory unitOfWork) : base(unitOfWork)
  {
  }
}
