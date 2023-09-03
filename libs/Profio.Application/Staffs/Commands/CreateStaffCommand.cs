using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Commands;
using Profio.Application.CQRS.Handlers.Command;
using Profio.Domain.Constants;
using Profio.Domain.Entities;

namespace Profio.Application.Staffs.Commands;

public record CreateStaffCommand : CreateCommandBase
{
  public required string? Name { get; set; }
  public string? Phone { get; set; }
  public Position Position { get; set; } = Position.Driver;
}
public class CreateStaffCommandHandler : CreateCommandHandlerBase<CreateStaffCommand, Staff>
{
  public CreateStaffCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
}
