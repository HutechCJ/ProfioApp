using MediatR;

namespace Profio.Application.Users.Commands.Register;

public record RegisterCommand(string UserName, string Email, string Password) : IRequest<AccountDTO>;
