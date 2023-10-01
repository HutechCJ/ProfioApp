using FluentValidation;
using MediatR;
using Profio.Domain.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace Profio.Application.Vehicles.Commands;

[SwaggerSchema(
  Title = "Update Vehicle Status",
  Description = "A Representation of Update Vehicle Status")]
public sealed record UpdateVehicleStatusCommand(object Id) : IRequest<Unit>
{
  public VehicleStatus? Status { get; set; }
}

public sealed class UpdateVehicleStatusCommandHandler : IRequestHandler<UpdateVehicleStatusCommand, Unit>
{
  private readonly ISender _sender;

  public UpdateVehicleStatusCommandHandler(ISender sender)
    => _sender = sender;
  public async Task<Unit> Handle(UpdateVehicleStatusCommand request, CancellationToken cancellationToken)
    => await _sender.Send(new UpdateVehicleCommand(request.Id) { Status = request.Status }, cancellationToken);
}

public class UpdateVehicleStatusCommandValidator : AbstractValidator<UpdateVehicleStatusCommand>
{
  public UpdateVehicleStatusCommandValidator()
    => RuleFor(x => x.Id).NotEmpty().NotNull();
}
