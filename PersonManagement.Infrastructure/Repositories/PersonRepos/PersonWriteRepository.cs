using PersonManagement.Infrastructure.Repositories.Base;
using PersonManagement.Domain;
using PersonManagement.Application.RepoInterfaces;
using Microsoft.Extensions.Logging;

namespace PersonManagement.Infrastructure.Repositories
{
    public class PersonWriteRepository : WriteRepository<Person>, IPersonWriteRepository
    {
        public PersonWriteRepository(ILogger<WriteRepository<Person>> logger, DataContext dbContext) : base(logger, dbContext)
        {
            
        }
    }
}
