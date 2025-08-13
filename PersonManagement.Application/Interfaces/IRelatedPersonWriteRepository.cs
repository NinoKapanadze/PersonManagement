using PersonManagement.Application.RepoInterfaces.Base;
using PersonManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Application.Interfaces
{
    public interface IRelatedPersonWriteRepository : IWriteRepository<RelatedPerson>
    {
    }
}
