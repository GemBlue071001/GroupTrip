namespace GT.AuthService.Application.Messaging;

public interface IEventSubscriber
{
  Task SubscribeAsync(string topic, CancellationToken cancellationToken);
}

