using Microsoft.Extensions.Logging;
using PersonManagement.Application.Interfaces;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Infrastructure.Repositories.RelatedPersonRepos
{
    public class RelatedPersonWriteRepository : WriteRepository<RelatedPerson>, IRelatedPersonWriteRepository
    {
        public RelatedPersonWriteRepository( DataContext dbContext) : base(dbContext)
        {

        }
    }
}
