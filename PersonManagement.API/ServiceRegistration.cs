using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonManagement.Infrastructure;
using System.Reflection;

namespace PersonManagement.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Person Management API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddDbContext<DataContext>(options =>
                                  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
