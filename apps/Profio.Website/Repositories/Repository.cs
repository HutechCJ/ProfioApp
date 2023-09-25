using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Profio.Website.Repositories;

public sealed class Repository : IRepository
{
  private readonly HttpClient _httpClient;

  public Repository(IHttpClientFactory httpClientFactory)
      => _httpClient = httpClientFactory.CreateClient("Api");

  public async Task<TResult?> GetAsync<TResult>(string route)
      => await DeserializeContentAsync<TResult>(await _httpClient.GetAsync(route));

  public async Task<TResult?> PostAsync<TResult>(string route, object? body)
  {
    var jsonContent = new StringContent(
        JsonSerializer.Serialize(body),
        Encoding.UTF8,
        MediaTypeNames.Application.Json);
    return await DeserializeContentAsync<TResult>(await _httpClient.PostAsync(route, jsonContent));
  }

  public async Task<TResult?> PatchAsync<TResult>(string route, object? body)
  {
    var jsonContent = new StringContent(
        JsonSerializer.Serialize(body),
        Encoding.UTF8,
        MediaTypeNames.Application.Json);
    return await DeserializeContentAsync<TResult>(await _httpClient.PatchAsync(route, jsonContent));
  }

  public async Task<TResult?> PutAsync<TResult>(string route, object? body)
  {
    var jsonContent = new StringContent(
        JsonSerializer.Serialize(body),
        Encoding.UTF8,
        MediaTypeNames.Application.Json);
    return await DeserializeContentAsync<TResult>(await _httpClient.PutAsync(route, jsonContent));
  }

  public async Task<TResult?> DeleteAsync<TResult>(string route)
      => await DeserializeContentAsync<TResult>(await _httpClient.DeleteAsync(route));

  private static async Task<TResult?> DeserializeContentAsync<TResult>(HttpResponseMessage response)
  {
    try
    {
      var serializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };
      return await JsonSerializer.DeserializeAsync<TResult>(await response.Content.ReadAsStreamAsync(), serializerOptions);
    }
    catch (ArgumentException)
    {
      return default;
    }
  }
}
