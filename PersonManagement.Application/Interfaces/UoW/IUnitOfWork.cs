using PersonManagement.Application.Interfaces;
using System.Net.Http.Headers;

namespace PersonManagement.Application.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        IPersonWriteRepository PersonWriteRepository { get; }
        IRelatedPersonWriteRepository RelatedPersonWriteRepository { get;}   
    }
}
