using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<Unit>;
public record LoginCommandHandler : IRequestHandler<LoginCommand, Unit>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public LoginCommandHandler(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }
  public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _userManager.FindByNameAsync(request.UserName);
    if (user == null)
    {
      // TODO: Add Unauthorize Exception and Handler in Middleware handler
      throw new NotImplementedException();
    }
    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
    if (!isPasswordCorrect)
    {
      // TODO: Add Unauthorize Exception and Handler in Middleware handler
      throw new NotImplementedException();
    }
    return Unit.Value;
  }
}
