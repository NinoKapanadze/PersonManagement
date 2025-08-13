using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Application.Interfaces;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Application.RepoInterfaces.Base;
using PersonManagement.Infrastructure.Repositories;
using PersonManagement.Infrastructure.Repositories.Base;
using PersonManagement.Infrastructure.Repositories.RelatedPersonRepos;
using PersonManagement.Infrastructure.UoW;

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

            return services;
        }
    }
}
