using System.Text.Json;
using Confluent.Kafka;

namespace GT.TripManagementService.Application.Messaging.Events;

public class KafkaEventPublisher : IEventPublisher
    {
        private readonly IProducer<string, string> _producer;

        public KafkaEventPublisher(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublishAsync<T>(string topic, T @event, CancellationToken cancellationToken = default)
        {
            var value = JsonSerializer.Serialize(@event);
            await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = value
            }, cancellationToken);
        }
    }
