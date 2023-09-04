using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Models;
using Profio.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users.Commands.Register;

[SwaggerSchema(
  Title = "Register Request",
  Description = "A Representation of Register Account")]
public record RegisterCommand(string Email, string Password, string FullName) : IRequest<ResultModel<AccountDto>>;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultModel<AccountDto>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;

  public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
    => (_userManager, _mapper, _tokenService) = (userManager, mapper, tokenService);

  public async Task<ResultModel<AccountDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    if (_userManager.Users.Any(u => u.UserName == request.Email))
      return ResultModel<AccountDto>.CreateError(null, "User name already exists");

    if (_userManager.Users.Any(u => u.Email == request.Email))
      return ResultModel<AccountDto>.CreateError(null, "Email already exists");

    var user = new ApplicationUser
    {
      UserName = request.Email,
      Email = request.Email,
      FullName = request.FullName,
    };

    var result = await _userManager.CreateAsync(user, request.Password);

    if (!result.Succeeded)
      return ResultModel<AccountDto>.CreateError(null, "Create user failed");
    
    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    return ResultModel<AccountDto>.Create(dto);
  }
}
