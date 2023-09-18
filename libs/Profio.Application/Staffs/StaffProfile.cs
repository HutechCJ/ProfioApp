using Profio.Application.Abstractions.CQRS;
using Profio.Application.Staffs.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Staffs;

public sealed class StaffProfile : EntityProfileBase<Staff, StaffDto, CreateStaffCommand, UpdateStaffCommand> { }
