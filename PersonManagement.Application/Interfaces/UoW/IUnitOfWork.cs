using PersonManagement.Application.Interfaces;
using System.Net.Http.Headers;

namespace PersonManagement.Application.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync(CancellationToken cancellationToken);
        IPersonWriteRepository PersonWriteRepository { get; }
        IRelatedPersonWriteRepository RelatedPersonWriteRepository { get;
        }   
    }
}
