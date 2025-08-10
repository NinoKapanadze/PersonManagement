using PersonManagement.Application.RepoInterfaces.Base;
using PersonManagement.Domain;

namespace PersonManagement.Application.RepoInterfaces
{
    public interface IPersonReadRepository : IReadRepository <Person>
    {
    }
}
