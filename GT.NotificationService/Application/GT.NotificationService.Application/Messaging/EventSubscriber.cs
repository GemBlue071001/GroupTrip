using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GT.NotificationService.Application.Messaging
{
    public class EventSubscriber : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<EventSubscriber> _logger;
        private readonly UserLoginCache _userLoginCache;

        public EventSubscriber(
            ConsumerConfig config,
            ILogger<EventSubscriber> logger,
            UserLoginCache userLoginCache)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _logger = logger;
            _userLoginCache = userLoginCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield(); // Allow the service to start

            _consumer.Subscribe("user.logged-in");
            _logger.LogInformation("Kafka consumer started. Listening to topic: user.logged-in");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(100));
                        
                        if (consumeResult?.Message?.Value != null)
                        {
                            _logger.LogInformation("Received message: {Message}", consumeResult.Message.Value);
                            
                            var loginEvent = JsonSerializer.Deserialize<UserLoginSuccessful>(
                                consumeResult.Message.Value,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            );

                            if (loginEvent != null)
                            {
                                _userLoginCache.AddUserLogin(loginEvent);
                                _logger.LogInformation("Processed login event for user: {UserName}", loginEvent.UserName);
                            }
                        }
                        
                        await Task.Delay(10, stoppingToken); // Small delay to prevent CPU spinning
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError(ex, "Error consuming message: {Reason}", ex.Error.Reason);
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error deserializing message");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unexpected error in Kafka consumer");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer is stopping.");
            }
            finally
            {
                try
                {
                    _consumer.Close();
                    _logger.LogInformation("Kafka consumer closed.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error closing Kafka consumer");
                }
            }
        }

        public override void Dispose()
        {
            _consumer?.Dispose();
            base.Dispose();
        }
    }

    public record UserLoginSuccessful(Guid UserId, string UserName, string Email, DateTime LoggedInAt);
}