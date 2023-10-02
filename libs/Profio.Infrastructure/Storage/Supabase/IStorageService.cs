using Microsoft.AspNetCore.Http;

namespace Profio.Infrastructure.Storage.Supabase;

public interface IStorageService
{
  public Task<string> UploadAsync(IFormFile file);
  public Task RemoveAsync(Guid id);
}
