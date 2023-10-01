using FluentValidation;
using System.Diagnostics;

namespace Profio.Infrastructure.Validator;

public static class Extension
{
  public static async Task HandleValidationAsync<TRequest>(this IValidator<TRequest> validator, TRequest request)
  {
    var validationResult = await validator.ValidateAsync(request);

    var failures = validationResult
          .Errors;

    if (failures.Any())
      throw new ValidationException(failures);
  }

  [DebuggerStepThrough]
  public static IServiceCollection AddValidators(this IServiceCollection services)
      => services.Scan(scan => scan
              .FromAssemblies(AssemblyReference.Assembly)
              .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
              .AsImplementedInterfaces()
              .WithTransientLifetime());
}
