using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Profio.Domain.Models;
using Profio.Infrastructure.Exceptions;
using System.Net;

namespace Profio.Infrastructure.Middleware;

public class ExceptionMiddleware : ExceptionFilterAttribute
{
  public override void OnException(ExceptionContext context)
  {
    HandleException(context);
    base.OnException(context);
  }

  private static void HandleException(ExceptionContext context)
  {
    context.HttpContext.Response.ContentType = "application/json";
    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    switch (context.Exception)
    {
      case ValidationException { Errors: { } } validationException:
        {
          var validationErrorModel = ResultModel<Dictionary<string, string[]>>.CreateError(validationException
              .Errors
              .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
              .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()));

          context.Result = new BadRequestObjectResult(validationErrorModel);
          break;
        }
      case NotFoundException notFoundException:
        {
          var notFoundErrorModel = ResultModel<string>.CreateError(notFoundException.Message, "Not Found Error.");
          context.Result = new NotFoundObjectResult(notFoundErrorModel);

          break;
        }
      default:
        var details = new ProblemDetails
        {
          Status = StatusCodes.Status500InternalServerError,
          Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };
        context.Result = new ObjectResult(details)
        {
          Value = ResultModel<string>.CreateError("", "Internal Server Error.").ToString()
        };
        break;
    }
    context.ExceptionHandled = true;
  }

}
