using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Infrastructure.Repositories.Base;

namespace PersonManagement.Infrastructure.Repositories
{
    public class PersonReadRepository : ReadRepository<Person>, IPersonReadRepository
    {
        public PersonReadRepository( DataContext dbContext) : base(dbContext)
        {
            
        }
    }
}
