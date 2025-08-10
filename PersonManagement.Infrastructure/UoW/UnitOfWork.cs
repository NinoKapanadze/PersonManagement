using Microsoft.Extensions.Logging;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Infrastructure.Repositories;
using PersonManagement.Infrastructure.Repositories.Base;

namespace PersonManagement.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dbContext;
         private readonly ILogger<WriteRepository<Person>> _personLogger;
        public IPersonWriteRepository PersonWriteRepository { get; private set; }

        public UnitOfWork(DataContext dbContext, ILogger<WriteRepository<Person>> personLogger)
        {
            _dbContext = dbContext;
            _personLogger = personLogger;

            PersonWriteRepository = new PersonWriteRepository(personLogger, dbContext);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
