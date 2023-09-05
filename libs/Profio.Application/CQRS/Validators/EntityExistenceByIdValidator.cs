using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentValidation;
using Profio.Domain.Interfaces;

namespace Profio.Application.CQRS.Validators;

public class EntityExistenceByIdValidator<TEntity> : AbstractValidator<object>
  where TEntity : class, IEntity<object>
{
  private readonly IRepository<TEntity> _repository;

  public EntityExistenceByIdValidator(IRepositoryFactory unitOfWork)
  {
    _repository = unitOfWork.Repository<TEntity>();

    RuleFor(x => x)
      .Cascade(CascadeMode.Stop)
      .MustAsync(ValidateId).WithMessage($"The specified {typeof(TEntity).Name} Id does not exist.");
  }

  private async Task<bool> ValidateId(object? id, CancellationToken cancellationToken)
  {
    if (id is null)
      return true;

    var isIdValid = await _repository.CountAsync(x => x.Id.Equals(id), cancellationToken) == 1;
    return isIdValid;
  }
}
