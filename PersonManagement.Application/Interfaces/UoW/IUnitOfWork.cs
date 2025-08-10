namespace PersonManagement.Application.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
        IPersonWriteRepository PersonWriteRepository { get; }
    }
}
