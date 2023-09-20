using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS.Events.Commands;
using Profio.Infrastructure.Abstractions.CQRS.Handlers.Command;
using Profio.Infrastructure.Abstractions.CQRS.Validators;

namespace Profio.Application.Staffs.Commands;

public sealed record DeleteStaffCommand(object Id) : DeleteCommandBase<StaffDto>(Id);

public sealed class DeleteStaffCommandHandler : DeleteCommandHandlerBase<DeleteStaffCommand, StaffDto, Staff>
{
  public DeleteStaffCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public sealed class DeleteStaffCommandValidator : DeleteCommandValidatorBase<DeleteStaffCommand, StaffDto>
{
}
