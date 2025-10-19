

namespace GT.NotificationService.Application.Messaging
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(string topic, T @event, CancellationToken cancellationToken = default);
    }
}
