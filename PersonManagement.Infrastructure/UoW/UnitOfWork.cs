using Microsoft.Extensions.Logging;
using PersonManagement.Application.Interfaces;
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
        public IRelatedPersonWriteRepository RelatedPersonWriteRepository { get; private set; }

        public UnitOfWork(DataContext dbContext, ILogger<WriteRepository<Person>> personLogger, IPersonWriteRepository personWriteRepository, IRelatedPersonWriteRepository relatedPersonWriteRepository )
        {
            _dbContext = dbContext;
            _personLogger = personLogger;

            //PersonWriteRepository = new PersonWriteRepository(personLogger, dbContext);
            RelatedPersonWriteRepository = relatedPersonWriteRepository;
            PersonWriteRepository = personWriteRepository;
        }

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
