using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Infrastructure.Configurations
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.StartDate)
                .IsRequired();
            builder.Property(e => e.EndDate)
                .IsRequired(false);
            builder.HasOne(e => e.Person)
                .WithMany(p => p.Experiences)
                .HasForeignKey(e => e.PersonId);
            builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
