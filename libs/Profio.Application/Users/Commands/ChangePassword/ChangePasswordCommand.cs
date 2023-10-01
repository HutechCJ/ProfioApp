using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Identity;
using Profio.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users.Commands.ChangePassword;

[SwaggerSchema(
  Title = "Change Password",
  Description = "A Representation of Change Password")]
public sealed record ChangePasswordCommand(string OldPassword, string NewPassword, string ConfirmPassword) : IRequest<AccountDto>;

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, AccountDto>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
  {
    _userManager = userManager;
    _mapper = mapper;
    _tokenService = tokenService;
    _httpContextAccessor = httpContextAccessor;
  }
  public async Task<AccountDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
  {
    var failures = new List<ValidationFailure>();
    if (_httpContextAccessor.HttpContext is null)
      throw new InvalidOperationException();
    if (_httpContextAccessor.HttpContext.User.Identity?.Name is null)
      throw new UnauthorizedAccessException(nameof(ApplicationUser));

    var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name!)
      ?? throw new UnauthorizedAccessException(nameof(ApplicationUser));

    if (!request.NewPassword.Equals(request.ConfirmPassword))
      failures.Add(new("Password", "Confirm password must match the new password"));

    if (failures.Any())
      throw new ValidationException(failures);

    var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
    if (!result.Succeeded)
    {
      result.Errors.Select(e => e.Description).ForEach(d => { failures.Add(new("Password", d)); });
      throw new ValidationException(failures);
    }

    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    dto.TokenExpire = _tokenService.GetExpireDate(dto.Token);
    return dto;
  }
}
