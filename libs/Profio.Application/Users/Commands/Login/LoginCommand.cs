using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Identity;
using Profio.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users.Commands.Login;

[SwaggerSchema(
  Title = "Account Request",
  Description = "A Representation of Account")]
public record LoginCommand(string UserName, string Password) : IRequest<AccountDto>;

public record LoginCommandHandler : IRequestHandler<LoginCommand, AccountDto>
{
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;
  private readonly UserManager<ApplicationUser> _userManager;

  public LoginCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
    => (_userManager, _mapper, _tokenService) = (userManager, mapper, tokenService);

  public async Task<AccountDto> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _userManager.FindByNameAsync(request.UserName)
               ?? throw new UnauthorizedAccessException("User not found!");
    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

    if (!isPasswordCorrect)
      throw new UnauthorizedAccessException("Password not correct!");

    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    dto.TokenExpire = _tokenService.GetExpireDate(dto.Token);
    return dto;
  }
}
