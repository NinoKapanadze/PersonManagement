using Microsoft.EntityFrameworkCore;
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

        public async Task<Person?> GetPersonWithDetailsAsync(int id)
        {
            return await _dbContext.Persons
                .Include(p => p.RelatedPersons).ThenInclude(rp => rp.RelatedTo)
                .Include(p => p.PhoneNumbers)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
