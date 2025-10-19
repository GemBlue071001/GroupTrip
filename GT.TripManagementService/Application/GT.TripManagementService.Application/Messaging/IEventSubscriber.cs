namespace GT.TripManagementService.Application.Messaging;

public interface IEventSubscriber
{
  Task SubscribeAsync(string topic, CancellationToken cancellationToken);
}

