using Microsoft.Extensions.Logging;
using PersonManagement.Application.Interfaces;
using PersonManagement.Domain;
using PersonManagement.Infrastructure.Repositories.Base;

namespace PersonManagement.Infrastructure.Repositories.RelatedPersonRepos
{
    public class RelatedPersonWriteRepository : WriteRepository<RelatedPerson>, IRelatedPersonWriteRepository
    {
        public RelatedPersonWriteRepository(DataContext dbContext, ILogger<WriteRepository<RelatedPerson>> logger) : base(dbContext, logger)
        {

        }
    }
}
