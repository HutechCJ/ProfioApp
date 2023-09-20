using Microsoft.AspNetCore.Http;

namespace Profio.Infrastructure.Storage.Supabase;

public interface IStorageService
{
  Task<string> UploadAsync(IFormFile file);
  Task RemoveAsync(Guid id);
}
