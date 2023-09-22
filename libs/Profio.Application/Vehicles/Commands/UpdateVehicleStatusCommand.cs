using FluentValidation;
using MediatR;
using Profio.Domain.Constants;

namespace Profio.Application.Vehicles.Commands;

public sealed record UpdateVehicleStatusCommand(object Id) : IRequest<Unit>
{
  public VehicleStatus? Status { get; set; }
}
sealed class UpdateVehicleStatusCommandHandler : IRequestHandler<UpdateVehicleStatusCommand, Unit>
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
