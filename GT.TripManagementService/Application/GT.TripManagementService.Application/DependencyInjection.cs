using Confluent.Kafka;
using GT.TripManagementService.Application;
using GT.TripManagementService.Application.Interface;
using GT.TripManagementService.Application.Messaging;
using GT.TripManagementService.Application.Messaging.Events;
using GT.TripManagementService.Application.Service;
using GT.TripManagementService.Infrastructure.Interface;
using GT.TripManagementService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using AutoMapper.Collection;
using AutoMapper.EquivalencyExpression;

namespace GT.TripManagementService.Application
{
  public static class DependencyInjection
  {
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddRepository();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
            }, Assembly.GetExecutingAssembly()); services.AddServices(configuration);
      services.AddSingleton<JwtSecurityTokenHandler>();
      services.AddMemoryCache();
      // === Kafka ===
      services.AddSingleton<IEventPublisher>(sp =>
      {
        var config = new ProducerConfig
        {
          BootstrapServers = configuration["Kafka:BootstrapServers"]
        };
        return new KafkaEventPublisher(config);
      });

    }
    public static void AddRepository(this IServiceCollection services)
    {
      services
         .AddScoped<IUnitOfWork, UnitOfWork>();

    }
    //private static void AddAutoMapper(this IServiceCollection services, Assembly assembly)
    //{
    //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    //}
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITripService,TripService>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<ITagService, TagService>();
        }


  }
}
