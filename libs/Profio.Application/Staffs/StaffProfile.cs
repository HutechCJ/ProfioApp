using Profio.Application.Staffs.Commands;
using Profio.Domain.Entities;
using Profio.Infrastructure.Abstractions.CQRS;

namespace Profio.Application.Staffs;

public sealed class StaffProfile : EntityProfileBase<Staff, StaffDto, CreateStaffCommand, UpdateStaffCommand> { }
