using FluentValidation;
using MediatR;
using Profio.Domain.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Incidents.Commands;

[SwaggerSchema(
  Title = "Update Incident Status",
  Description = "A Representation of UpdateIncidentStatusCommand")]
public sealed record UpdateIncidentStatusCommand(object Id) : IRequest<Unit>
{
  public required IncidentStatus Status { get; set; }
}

public sealed class UpdateIncidentStatusCommandHandler : IRequestHandler<UpdateIncidentStatusCommand, Unit>
{
  private readonly ISender _sender;

  public UpdateIncidentStatusCommandHandler(ISender sender)
    => _sender = sender;

  public async Task<Unit> Handle(UpdateIncidentStatusCommand request, CancellationToken cancellationToken)
    => await _sender.Send(new UpdateIncidentCommand(request.Id) { Status = request.Status }, cancellationToken);
}

public class UpdateIncidentStatusCommandValidator : AbstractValidator<UpdateIncidentStatusCommand>
{
  public UpdateIncidentStatusCommandValidator()
    => RuleFor(x => x.Id).NotEmpty().NotNull();
}
