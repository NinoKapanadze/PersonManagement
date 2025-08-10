using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Persistance.Configurations
{
    public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            throw new NotImplementedException();
        }
    }
}
