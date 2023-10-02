using FluentValidation;
using Microsoft.AspNetCore.Http;
using Supabase;

namespace Profio.Infrastructure.Storage.Supabase.Internals;

public sealed class StorageService : IStorageService
{
  private readonly Client _client;

  public StorageService(Client client)
    => _client = client;

  public async Task<string> UploadAsync(IFormFile file)
  {
    using var stream = new MemoryStream();
    await file.CopyToAsync(stream);
    var lastDot = file.FileName.LastIndexOf('.');
    var ext = file.FileName[(lastDot + 1)..];
    var fileName = $"{Guid.NewGuid()}.{ext}";
    await _client.Storage.From("avatar")
      .Upload(stream.ToArray(), fileName);
    return _client.Storage.From("avatar").GetPublicUrl(fileName, new()
    {
      Width = 512,
      Height = 512
    });
  }

  public async Task RemoveAsync(Guid id)
    => await _client.Storage.From("avatar").Remove(id.ToString());
}

public class FileValidator : AbstractValidator<IFormFile>
{
  public FileValidator()
  {
    RuleFor(file => file.Length)
      .LessThanOrEqualTo(2 * 1024 * 1024)
      .WithMessage("File size must be less than or equal to 2MB.");

    RuleFor(file => file.ContentType)
      .Must(contentType => contentType is "image/jpeg" or "image/png")
      .WithMessage("File must be a JPEG or PNG image.");
  }
}
