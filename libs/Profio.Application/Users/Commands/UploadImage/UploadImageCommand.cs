using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Profio.Domain.Identity;
using Profio.Infrastructure.Storage.Supabase;

namespace Profio.Application.Users.Commands.UploadImage;

public sealed record UploadImageCommand(object UserId, IFormFile File) : IRequest<string>;

public sealed class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, string>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IStorageService _storageService;

  public UploadImageCommandHandler(
    UserManager<ApplicationUser> userManager,
    IStorageService storageService)
    => (_userManager, _storageService) = (userManager, storageService);

  public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
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

    await _userManager.UpdateAsync(user);

    return image;
  }
}
