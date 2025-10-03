using Microsoft.Extensions.Logging;
using PersonManagement.Application.Interfaces;
using PersonManagement.Domain;
using PersonManagement.Domain.Entities;
using PersonManagement.Infrastructure.Repositories.Base;

namespace PersonManagement.Infrastructure.Repositories.ExperienceRepos
{
    public class ExperienceWriteRepository  : WriteRepository<Experience>, IExperienceWriteRepository
    {
        public ExperienceWriteRepository(DataContext dbContext, ILogger<WriteRepository<Experience>> logger) : base(dbContext, logger)
        {
            
        }
    }
}
