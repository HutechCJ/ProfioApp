using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Profio.Infrastructure.Key;

public class ApiKeyAttribute : ServiceFilterAttribute
{
  public ApiKeyAttribute()
    : base(typeof(ApiKeyAuthFilter))
  {
  }
}

public class ApiKeyAuthFilter : IAuthorizationFilter
{
  private readonly IApiKeyValidation _apiKeyValidation;

  public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
    => _apiKeyValidation = apiKeyValidation;

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    var userApiKey = context.HttpContext.Request.Headers[ApiKey.ApiKeyHeaderName].ToString();

    if (string.IsNullOrWhiteSpace(userApiKey))
    {
      context.Result = new UnauthorizedResult();
      return;
    }

    if (!_apiKeyValidation.IsValidApiKey(userApiKey))
      context.Result = new UnauthorizedResult();
  }
}
