using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Identity;
using Profio.Infrastructure.Auth;
using Profio.Infrastructure.Storage.Supabase;

namespace Profio.Application.Users.Commands.UploadImage;

public sealed record UploadImageCommand(object UserId, IFormFile File) : IRequest<AccountDto>;

public sealed class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, AccountDto>
{
  private readonly IMapper _mapper;
  private readonly ITokenService _tokenService;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IStorageService _storageService;

  public UploadImageCommandHandler(
    UserManager<ApplicationUser> userManager,
    IMapper mapper,
    ITokenService tokenService,
    IStorageService storageService)
  {
    _userManager = userManager;
    _mapper = mapper;
    _tokenService = tokenService;
    _storageService = storageService;
  }

  public async Task<AccountDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
  {
    var failures = new List<ValidationFailure>();
    if (!_userManager.Users.Any(u => ReferenceEquals(u.Id, request.UserId)))
      failures.Add(new("UserName", "User is not exists"));

    if (failures.Any())
      throw new ValidationException(failures);

    var image = await _storageService.UploadAsync(request.File);

    var user = await _userManager.FindByIdAsync(request.UserId.ToString() ?? throw new InvalidOperationException())
               ?? throw new InvalidOperationException();
    user.ImageUrl = image;

    var result = await _userManager.UpdateAsync(user);

    if (!result.Succeeded)
    {
      result.Errors.Select(e => e.Description).ForEach(d => { failures.Add(new("Password", d)); });
      throw new ValidationException(failures);
    }

    var dto = _mapper.Map<AccountDto>(user);
    dto.Token = _tokenService.CreateToken(user);
    return dto;
  }
}
