using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Profio.Application.Vehicles.Validators;
using Profio.Domain.Constants;
using Profio.Domain.Exceptions;
using Profio.Domain.Identity;
using Profio.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Users.Commands.Register;

[SwaggerSchema(
  Title = "Create Account",
  Description = "A Representation of Register Account")]
public sealed record RegisterCommand
  (string Email, string FullName, string Password, string ConfirmPassword, string? StaffId) : IRequest<AccountDto>;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountDto>
{
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;
  private readonly UserManager<ApplicationUser> _userManager;

  public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
    => (_userManager, _mapper, _tokenService) = (userManager, mapper, tokenService);

  public async Task<AccountDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    var failures = new List<ValidationFailure>();
    if (_userManager.Users.Any(u => u.UserName == request.Email))
      failures.Add(new("UserName", "UserName has already exists"));

    if (_userManager.Users.Any(u => u.Email == request.Email))
      failures.Add(new("Email", "Email has already exists"));

    if (!request.Password.Equals(request.ConfirmPassword))
      failures.Add(new("Password", "Confirm password must match the password"));

    if (failures.Any())
      throw new ValidationException(failures);

    var user = new ApplicationUser
    {
      UserName = request.Email,
      Email = request.Email,
      FullName = request.FullName,
      StaffId = request.StaffId,
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    if (!result.Succeeded)
    {
      result.Errors.Select(e => e.Description).ForEach(d => { failures.Add(new("Password", d)); });
      throw new ValidationException(failures);
    }

    var newUser = await _userManager.Users
      .AsNoTracking()
      .Include(x => x.Staff)
      .SingleOrDefaultAsync(x => x.Id == user.Id, cancellationToken) ?? throw new NotFoundException(nameof(ApplicationUser), user.Id);

    await _userManager.AddToRoleAsync(newUser, UserRole.Officer);

    var dto = _mapper.Map<AccountDto>(newUser);
    dto.Token = _tokenService.CreateToken(user);
    return dto;
  }
}

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
  public RegisterCommandValidator(StaffExistenceByIdValidator staffValidator)
  {
    RuleFor(x => x.StaffId)
      .SetValidator(staffValidator!);

    RuleFor(r => r.Email)
      .EmailAddress();
  }
}
