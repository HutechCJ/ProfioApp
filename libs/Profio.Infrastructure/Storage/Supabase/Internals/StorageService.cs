using FluentValidation;
using Microsoft.AspNetCore.Http;
using Client = Supabase.Client;

namespace Profio.Infrastructure.Storage.Supabase.Internals;

public sealed class StorageService : IStorageService
{
  private readonly Client _client;
  private readonly string _bucketName;

  public StorageService(Client client, Bucket bucketName)
  {
    _client = client;
    _bucketName = bucketName switch
    {
      Bucket.Avatar => "avatar",
      Bucket.Vehicle => "vehicle",
      Bucket.Document => "document",
      Bucket.LicensePlate => "license-plate",
      Bucket.Incident => "incident",
      _ => throw new ArgumentOutOfRangeException(nameof(bucketName), bucketName, "Invalid bucket type.")
    };
  }

  public async Task<string> UploadAsync(IFormFile file)
  {
    using var stream = new MemoryStream();
    await file.CopyToAsync(stream);
    var lastDot = file.FileName.LastIndexOf('.');
    var ext = file.FileName[(lastDot + 1)..];
    var fileName = $"{Guid.NewGuid()}.{ext}";
    await _client.Storage.From(_bucketName)
      .Upload(stream.ToArray(), fileName);
    return _client.Storage.From(_bucketName).GetPublicUrl(fileName, new()
    {
      Width = 512,
      Height = 512,
    });
  }

  public async Task RemoveAsync(Guid id)
    => await _client.Storage.From(_bucketName).Remove(id.ToString());
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
