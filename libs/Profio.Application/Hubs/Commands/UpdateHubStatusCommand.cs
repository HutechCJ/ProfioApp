using FluentValidation;
using MediatR;
using Profio.Domain.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Hubs.Commands;

[SwaggerSchema(
  Title = "Update Hub Status",
  Description = "Update Hub Status")]
public sealed record UpdateHubStatusCommand(object Id) : IRequest<Unit>
{
  public HubStatus? Status { get; set; }
}

public sealed class UpdateHubStatusCommandHandler : IRequestHandler<UpdateHubStatusCommand, Unit>
{
  private readonly ISender _sender;

  public UpdateHubStatusCommandHandler(ISender sender)
    => _sender = sender;
  public async Task<Unit> Handle(UpdateHubStatusCommand request, CancellationToken cancellationToken)
    => await _sender.Send(new UpdateHubCommand(request.Id) { Status = request.Status }, cancellationToken);
}

public class UpdateHubStatusCommandValidator : AbstractValidator<UpdateHubStatusCommand>
{
  public UpdateHubStatusCommandValidator()
    => RuleFor(x => x.Id).NotEmpty().NotNull();
}
