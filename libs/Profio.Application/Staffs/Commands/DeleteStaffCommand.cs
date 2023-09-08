using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.Abstractions.CQRS.Events.Commands;
using Profio.Application.Abstractions.CQRS.Handlers.Command;
using Profio.Application.Abstractions.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Staffs.Commands;

public record DeleteStaffCommand(object Id) : DeleteCommandBase<StaffDto>(Id);

public class DeleteStaffCommandHandler : DeleteCommandHandlerBase<DeleteStaffCommand, StaffDto, Staff>
{
  public DeleteStaffCommandHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}

public class DeleteStaffCommandValidator : DeleteCommandValidatorBase<DeleteStaffCommand, StaffDto>
{
}
