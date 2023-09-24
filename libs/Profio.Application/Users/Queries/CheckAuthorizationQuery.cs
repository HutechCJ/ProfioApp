using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Profio.Domain.Identity;
using Profio.Infrastructure.Auth;

namespace Profio.Application.Users.Queries;

public sealed record CheckAuthorizationQuery : IRequest<AccountDto>;
public sealed class CheckAuthorizationQueryHandler : IRequestHandler<CheckAuthorizationQuery, AccountDto>
{
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IUserAccessor _userAccessor;

  public CheckAuthorizationQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService, IUserAccessor userAccessor)
    => (_userManager, _mapper, _tokenService, _userAccessor) = (userManager, mapper, tokenService, userAccessor);
  public async Task<AccountDto> Handle(CheckAuthorizationQuery request, CancellationToken cancellationToken)
  {
    var failures = new List<ValidationFailure>();

    var user = await _userManager.Users
      .Include(x => x.Staff)
      .SingleOrDefaultAsync(x => x.UserName != null && x.UserName.Equals(_userAccessor.UserName), cancellationToken)

      ?? throw new UnauthorizedAccessException(nameof(ApplicationUser));

    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    dto.TokenExpire = _tokenService.GetExpireDate(dto.Token);
    return dto;
  }
}
