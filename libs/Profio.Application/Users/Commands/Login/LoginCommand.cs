using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Models;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<ResultModel<AccountDTO>>;
public record LoginCommandHandler : IRequestHandler<LoginCommand, ResultModel<AccountDTO>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;

  public LoginCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    => (_userManager, _mapper) = (userManager, mapper);

  public async Task<ResultModel<AccountDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _userManager.FindByNameAsync(request.UserName);
    if (user == null)
      return ResultModel<AccountDTO>.CreateError(null, errorMessage: "User not found");
    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
    return !isPasswordCorrect
      ? ResultModel<AccountDTO>.CreateError(null, errorMessage: "Password not correct!")
      : ResultModel<AccountDTO>.Create(_mapper.Map<AccountDTO>(user));
  }
}
