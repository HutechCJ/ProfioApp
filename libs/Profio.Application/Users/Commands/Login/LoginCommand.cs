using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Identity;
using Profio.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users.Commands.Login;

[SwaggerSchema(
  Title = "Account Request",
  Description = "A Representation of Account")]
public sealed record LoginCommand(string UserName, string Password) : IRequest<AccountDto>;

public sealed record LoginCommandHandler : IRequestHandler<LoginCommand, AccountDto>
{
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;
  private readonly UserManager<ApplicationUser> _userManager;

  public LoginCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
    => (_userManager, _mapper, _tokenService) = (userManager, mapper, tokenService);

  public async Task<AccountDto> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _userManager.Users
      .Include(x => x.Staff)
      .SingleOrDefaultAsync(x => x.UserName != null && x.UserName.Equals(request.UserName), cancellationToken)
               ?? throw new UnauthorizedAccessException("Incorrect Email or Password!");
    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

    if (!isPasswordCorrect)
      throw new UnauthorizedAccessException("Incorrect Email or Password!");

    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    dto.TokenExpire = _tokenService.GetExpireDate(dto.Token);
    return dto;
  }
}
