

namespace GT.NotificationService.Application.Messaging
{
    public interface IEventSubscriber
    {
        Task SubscribeAsync(string topic, CancellationToken cancellationToken);
    }
}
