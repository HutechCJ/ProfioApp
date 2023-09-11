using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Profio.Domain.Models;
using Profio.Infrastructure.Exceptions;

namespace Profio.Infrastructure.Filters;

public sealed class ExceptionFilter : ExceptionFilterAttribute
{
  public override void OnException(ExceptionContext context)
  {
    HandleException(context);
    base.OnException(context);
  }

  private static void HandleException(ExceptionContext context)
  {
    context.HttpContext.Response.ContentType = "application/json";

    switch (context.Exception)
    {
      case ValidationException { Errors: not null } validationException:
        HandleValidationException(context, validationException);
        break;
      case NotFoundException notFoundException:
        HandleNotFoundException(context, notFoundException);
        break;
      case UnauthorizedAccessException unauthorizedAccessException:
        HandleUnauthorizedAccessException(context, unauthorizedAccessException);
        break;
      default:
        HandleDefaultException(context);
        break;
    }
    context.ExceptionHandled = true;
  }

  private static void HandleValidationException(ExceptionContext context, ValidationException validationException)
  {
    var validationErrorModel = ResultModel<Dictionary<string, string[]>>.CreateError(validationException
              .Errors
              .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
              .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()));

    context.Result = new BadRequestObjectResult(validationErrorModel);
  }

  private static void HandleNotFoundException(ExceptionContext context, Exception notFoundException)
  {
    var notFoundErrorModel = ResultModel<string>.CreateError(notFoundException.Message, "Not Found Error.");
    context.Result = new NotFoundObjectResult(notFoundErrorModel);
  }

  private static void HandleUnauthorizedAccessException(ExceptionContext context, Exception unauthorizedAccessException)
  {
    var unauthorizedErrorModel = ResultModel<string>.CreateError(unauthorizedAccessException.Message, "Unauthorized Error.");
    context.Result = new UnauthorizedObjectResult(unauthorizedErrorModel);
  }

  private static void HandleDefaultException(ExceptionContext context)
  {
    var details = new ProblemDetails
    {
      Status = StatusCodes.Status500InternalServerError,
      Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
    };
    context.Result = new ObjectResult(details)
    {
      StatusCode = StatusCodes.Status500InternalServerError,
      Value = ResultModel<string>.CreateError(null, "Internal Server Error.")
    };
  }
}
