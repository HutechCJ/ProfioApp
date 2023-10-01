using FluentValidation;
using MediatR;
using System.Text.Json;

namespace Profio.Infrastructure.Validator;

public sealed class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
  private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;
  private readonly IServiceProvider _serviceProvider;

  public RequestValidationBehavior(IServiceProvider serviceProvider,
      ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
      => (_serviceProvider, _logger) = (serviceProvider, logger);

  public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
  {
    _logger.LogInformation(
        "[{Prefix}] Handle request={X-RequestData} and response={X-ResponseData}",
        nameof(RequestValidationBehavior<TRequest, TResponse>), typeof(TRequest).Name, typeof(TResponse).Name);

    _logger.LogDebug(
        "Handled {Request} with content {X-RequestData}",
        typeof(TRequest).FullName, JsonSerializer.Serialize(request));

    var validators = _serviceProvider
      .GetService<IEnumerable<IValidator<TRequest>>>()?.ToList()
                     ?? throw new InvalidOperationException();

    if (validators.Any())
      await Task.WhenAll(
          validators.Select(v =>
              v.HandleValidationAsync(request)));

    var response = await next();

    _logger.LogInformation(
        "Handled {FullName} with content {Response}",
        typeof(TResponse).FullName, JsonSerializer.Serialize(response));

    return response;
  }
}
