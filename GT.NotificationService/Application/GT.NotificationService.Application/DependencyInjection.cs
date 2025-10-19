using Confluent.Kafka;
using GT.NotificationService.Application.Interfaces;
using GT.NotificationService.Application.Messaging;
using GT.NotificationService.Infrastructure.Interface;
using GT.NotificationService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace GT.NotificationService.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepository();
            services.AddAutoMapper();
            services.AddServices(configuration);
            services.AddMemoryCache();
        }

        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { cfg.AddMaps(Assembly.GetExecutingAssembly()); });
        }

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<UserLoginCache>();
            services.AddScoped<INotificationService, Notification.Application.Services.NotificationService>();
            services.AddSingleton(sp =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = configuration["Kafka:BootstrapServers"],
                    GroupId = "notification-service-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = true
                };
                return config;
            });
            services.AddHostedService<EventSubscriber>();
        }
    }
}