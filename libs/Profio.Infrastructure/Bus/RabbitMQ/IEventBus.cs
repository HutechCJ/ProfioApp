namespace Profio.Infrastructure.Bus.RabbitMQ;

public interface IEventBus
{
  Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
    where T : class;
}
