using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Models;
using Profio.Infrastructure.Identity;
//using Profio.Infrastructure.Validator;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users.Commands.Register;

[SwaggerSchema(
  Title = "Register Request",
  Description = "A Representation of Register Account")]
public record RegisterCommand(string Email, string FullName, string Password, string ConfirmPassword) : IRequest<ResultModel<AccountDto>>;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultModel<AccountDto>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;

  public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
    => (_userManager, _mapper, _tokenService) = (userManager, mapper, tokenService);

  public async Task<ResultModel<AccountDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    var failures = new List<ValidationFailure>();
    if (_userManager.Users.Any(u => u.UserName == request.Email))
      failures.Add(new ValidationFailure("UserName", "UserName has already exists"));

    if (_userManager.Users.Any(u => u.Email == request.Email))
      failures.Add(new ValidationFailure("Email", "Email has already exists"));

    if (failures.Any())
      throw new ValidationException(failures);

    var user = new ApplicationUser
    {
      UserName = request.Email,
      Email = request.Email,
      FullName = request.FullName,
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    if (!result.Succeeded)
    {
      result.Errors.Select(e => e.Description).ForEach(d =>
      {
        failures.Add(new ValidationFailure("User", d));
      });
      throw new ValidationException(failures);
    }
    //return ResultModel<AccountDto>.CreateError(null, "Create user failed");

    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    return ResultModel<AccountDto>.Create(dto);
  }
}
