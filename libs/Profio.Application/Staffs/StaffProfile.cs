using Profio.Application.CQRS;
using Profio.Application.Staffs.Commands;
using Profio.Domain.Entities;

namespace Profio.Application.Staffs;

public class StaffProfile : EntityProfileBase<Staff, StaffDto, CreateStaffCommand, UpdateStaffCommand> { }
