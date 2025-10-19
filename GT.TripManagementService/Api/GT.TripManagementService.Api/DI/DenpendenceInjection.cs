﻿
using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Infrastructure.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace GT.TripManagementService.Api.DI
{
    
        public static class DependencyInjection
        {
            public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            {
                services.ConfigSwagger();
                services.AddDatabase(configuration);
                services.ConfigRoute();
                //services.AddIdentity(configuration);
                services.AddHttpContextAccessor();
                services.AddMemoryCache();
                services.Logger(configuration);
            }
        //public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        //    {
        //        options.Password.RequireDigit = true;
        //        options.Password.RequireUppercase = true;
        //        options.Password.RequireLowercase = true;
        //        options.Password.RequireNonAlphanumeric = true;
        //        options.Password.RequiredLength = 6;
        //    })
        //    .AddEntityFrameworkStores<GroupTripDbContext>()
        //    .AddDefaultTokenProviders();
        
        //}
       
            

            public static void ConfigRoute(this IServiceCollection services)
            {
                services.Configure<RouteOptions>(options =>
                {
                    options.LowercaseUrls = true;
                });
            }

            public static void ConfigSwagger(this IServiceCollection services)
            {
                // config swagger
                services.AddSwaggerGen(c =>
            
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v1",
                        Title = "API"

                    });

                    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    //c.IncludeXmlComments(xmlPath);
                    // Thêm JWT Bearer Token vào Swagger
                    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description = "JWT Authorization header sử dụng scheme Bearer.",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Name = "Authorization",
                        Scheme = "bearer"
                    });
                    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                    c.OrderActionsBy((apiDesc) =>
                    {
                        if (apiDesc.HttpMethod == "POST") return "3";
                        if (apiDesc.HttpMethod == "GET") return "1";
                        if (apiDesc.HttpMethod == "PUT") return "2";
                        if (apiDesc.HttpMethod == "DELETE") return "4";
                        return "5";
                    });
                });
            }
            public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddDbContext<TripDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                });
            }
            

            public static void Logger(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddSingleton<ILogger>(sp =>
                {
                    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                    return loggerFactory.CreateLogger("GlobalLogger");
                });
            }
        }
    }
