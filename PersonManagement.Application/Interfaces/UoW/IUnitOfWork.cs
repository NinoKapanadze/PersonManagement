using PersonManagement.Application.Interfaces;

namespace PersonManagement.Application.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
        IPersonWriteRepository PersonWriteRepository { get; }
        IRelatedPersonWriteRepository RelatedPersonWriteRepository { get;
        }   
    }
}
