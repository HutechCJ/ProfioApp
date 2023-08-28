using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Application.CQRS.Models;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users.Commands.Register;

public record RegisterCommand(string Email, string Password, string FullName) : IRequest<ResultModel<AccountDTO>>;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultModel<AccountDTO>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;

  public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    => (_userManager, _mapper) = (userManager, mapper);

  public async Task<ResultModel<AccountDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    if (_userManager.Users.Any(u => u.UserName == request.Email))
      return ResultModel<AccountDTO>.CreateError(null, "User name already exists");
    if (_userManager.Users.Any(u => u.Email == request.Email))
      return ResultModel<AccountDTO>.CreateError(null, "Email already exists");
    var user = new ApplicationUser
    {
      UserName = request.Email,
      Email = request.Email,
      FullName = request.FullName,
    };

    var result = await _userManager.CreateAsync(user, request.Password);

    if (!result.Succeeded)
      return ResultModel<AccountDTO>.CreateError(null, "Create user failed");
    return ResultModel<AccountDTO>.Create(_mapper.Map<AccountDTO>(user));
  }
}
