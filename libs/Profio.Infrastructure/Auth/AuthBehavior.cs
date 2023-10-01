using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Profio.Domain.Exceptions;

namespace Profio.Infrastructure.Auth;

public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>, IAuthRequest
       where TResponse : notnull
{
  private readonly IAuthorizationService _authorizationService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IEnumerable<IAuthorizationRequirement> _authorizationRequirements;
  private readonly ILogger<AuthBehavior<TRequest, TResponse>> _logger;

  public AuthBehavior(
      IAuthorizationService authorizationService,
      IHttpContextAccessor httpContextAccessor,
      IEnumerable<IAuthorizationRequirement> authorizationRequirements,
      ILogger<AuthBehavior<TRequest, TResponse>> logger)
  {
    _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    _authorizationRequirements = authorizationRequirements ?? throw new ArgumentNullException(nameof(authorizationRequirements));
    _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
  {
    if (request is not IAuthRequest authRequest)
      return await next();

    _logger.LogInformation("[{Prefix}] Starting AuthBehavior", nameof(AuthBehavior<TRequest, TResponse>));
    var user = (_httpContextAccessor.HttpContext?.User) ?? throw new AuthException();

    var result = await _authorizationService.AuthorizeAsync(
      user,
      null,
      _authorizationRequirements.Where(_ => false).ToList());

    if (!result.Succeeded)
      throw new UnauthorizedAccessException();

    return await next();
  }
}
