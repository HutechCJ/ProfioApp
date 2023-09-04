using MassTransit;

namespace Profio.Infrastructure.Bus.RabbitMQ.Internal;

public sealed class EventBus : IEventBus
{
  private readonly IPublishEndpoint _publishEndpoint;

  public EventBus(IPublishEndpoint publishEndpoint)
    => _publishEndpoint = publishEndpoint;

  public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class
    => _publishEndpoint.Publish(@event, cancellationToken);
}
