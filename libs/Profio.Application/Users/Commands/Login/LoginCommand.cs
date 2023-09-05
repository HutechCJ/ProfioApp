using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Models;
using Profio.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users.Commands.Login;

[SwaggerSchema(
  Title = "Account Request",
  Description = "A Representation of Account")]
public record LoginCommand(string UserName, string Password) : IRequest<ResultModel<AccountDto>>;
public record LoginCommandHandler : IRequestHandler<LoginCommand, ResultModel<AccountDto>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;

  public LoginCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
    => (_userManager, _mapper, _tokenService) = (userManager, mapper, tokenService);

  public async Task<ResultModel<AccountDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _userManager.FindByNameAsync(request.UserName)
      ?? throw new UnauthorizedAccessException("User not found!");
    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

    if (!isPasswordCorrect)
      throw new UnauthorizedAccessException("Password not correct!");

    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    dto.TokenExpire = _tokenService.GetExpireDate(dto.Token);
    return ResultModel<AccountDto>.Create(dto);
  }
}
