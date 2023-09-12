using Microsoft.AspNetCore.Http;
using Profio.Infrastructure.Storage.Supabase.Internals;

namespace Profio.Infrastructure.Storage.Supabase;

public interface IStorageService
{
  Task<string> UploadAsync(IFormFile file);
  Task RemoveAsync(Guid id);
}
