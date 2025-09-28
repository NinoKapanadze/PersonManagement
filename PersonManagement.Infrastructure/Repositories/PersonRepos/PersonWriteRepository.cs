using Microsoft.Extensions.Logging;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Infrastructure.Repositories.Base;

namespace PersonManagement.Infrastructure.Repositories
{
    public class PersonWriteRepository : WriteRepository<Person>, IPersonWriteRepository
    {
        public PersonWriteRepository(DataContext dbContext, ILogger<WriteRepository<Person>> logger) : base(dbContext, logger)
        {

        }
    }
}
