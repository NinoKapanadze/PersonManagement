using Microsoft.EntityFrameworkCore.Storage;
using PersonManagement.Application.Interfaces;
using PersonManagement.Application.RepoInterfaces;

namespace PersonManagement.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dbContext;
        private IDbContextTransaction? _currentTransaction;
        public IPersonWriteRepository PersonWriteRepository { get; private set; }
        public IRelatedPersonWriteRepository RelatedPersonWriteRepository { get; private set; }

        public IExperienceWriteRepository ExperienceWriteRepository { get; private set; }

        public UnitOfWork(DataContext dbContext,
                      IPersonWriteRepository personWriteRepository,
                      IRelatedPersonWriteRepository relatedPersonWriteRepository)
        {
            _dbContext = dbContext;
            PersonWriteRepository = personWriteRepository;
            RelatedPersonWriteRepository = relatedPersonWriteRepository;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction == null)
                throw new InvalidOperationException("No active transaction to commit.");

            await _currentTransaction.CommitAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction == null)
                throw new InvalidOperationException("No active transaction to rollback.");

            await _currentTransaction.RollbackAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _currentTransaction?.Dispose();
        }
    }
}

