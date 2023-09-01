using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Profio.Domain.Models;
using Profio.Infrastructure.Excepitions;
using Profio.Infrastructure.Validator;
using System.Net;

namespace Profio.Infrastructure.Middleware;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    => (_next, _logger) = (next, logger);

  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Something went wrong");
      await HandleExceptionAsync(httpContext, ex);
    }
  }

  private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    if (exception is ValidationException { ValidationResultModel.Errors: { } } validationException)
    {
      var validationErrorModel = ResultModel<string>.CreateError(validationException.ValidationResultModel
          .Errors
          .Aggregate("", (a, b) =>
            a + $"{b.Field}-{b.Message}\n"), "Validation Error.")
        .ToString();

      await context.Response.WriteAsync(validationErrorModel);
    }
    else if (exception is NotFoundException notFoundException)
    {
      var notFoundErrorModel = ResultModel<string>.CreateError(notFoundException.Message, "Not Found Error.")
        .ToString();
      await context.Response.WriteAsync(notFoundErrorModel);
    }
    else
      await context.Response.WriteAsync(
        ResultModel<string>.CreateError("", "Internal Server Error.").ToString());
  }
}
