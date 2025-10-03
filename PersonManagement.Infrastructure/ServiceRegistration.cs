using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Application.Interfaces;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Application.RepoInterfaces.Base;
using PersonManagement.Infrastructure.Repositories;
using PersonManagement.Infrastructure.Repositories.Base;
using PersonManagement.Infrastructure.Repositories.RelatedPersonRepos;
using PersonManagement.Infrastructure.Seeding;
using PersonManagement.Infrastructure.Services;
using PersonManagement.Infrastructure.UoW;
using System;

namespace PersonManagement.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection Addinfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonReadRepository, PersonReadRepository>();
            services.AddScoped<IPersonWriteRepository, PersonWriteRepository>();

            services.AddScoped<IRelatedPersonWriteRepository, RelatedPersonWriteRepository>();

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CacheService>();

            services.AddDbContext<DataContext>(options =>
                                  options.UseSqlServer(
                                      configuration.GetConnectionString("DefaultConnection"),
                                       sqlOptions =>
                                       {
                                           sqlOptions.EnableRetryOnFailure(
                                               maxRetryCount: 2,              
                                               maxRetryDelay: TimeSpan.FromSeconds(5), 
                                               errorNumbersToAdd: null     
                                           );
                                       }
                                  )
            );

            var cacheConn = configuration.GetConnectionString("Cache");
            if (!string.IsNullOrWhiteSpace(cacheConn))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                  options.Configuration = cacheConn;
                    options.InstanceName = "PersonManagement_";
                });
            }  
            else
            {
                services.AddDistributedMemoryCache();
            }

            return services;
        }
        public static async Task SeedDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await DbSeeder.SeedAsync(unitOfWork, dbContext);
        }
    }
}
