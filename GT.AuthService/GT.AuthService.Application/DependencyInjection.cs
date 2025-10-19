using Confluent.Kafka;
using GT.AuthService.Application;
using GT.AuthService.Application.Interfaces;
using GT.AuthService.Application.Messaging;
using GT.AuthService.Application.Messaging.Events;
using GT.AuthService.Application.Services;
using GT.AuthService.Domain.Utils;
using GT.AuthService.Infrastructure.Interface;
using GT.AuthService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;


namespace GT.AuthService.Application
{
  public static class DependencyInjection
  {
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddRepository();
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddServices(configuration);
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
      services.AddScoped<IAuthenService, AuthenService>();

      services.AddSingleton<Authentication>();
      services.AddScoped<IRoleService, RoleService>();
    }


  }
}
