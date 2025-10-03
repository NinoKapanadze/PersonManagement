using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain;

namespace PersonManagement.Infrastructure.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasMany(p => p.RelatedPersons)
                .WithOne(rp => rp.Person)
                .HasForeignKey(rp => rp.PersonId);

            builder.Property(p => p.FirstName)
             .IsRequired()
             .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Gender)
                .IsRequired(false);

            builder.Property(p => p.PersonalIdNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(p => p.PersonalIdNumber)
                .IsUnique();

            builder.Property(p => p.BirthDay)
                .IsRequired();

            builder.Property(p=>p.ProfessionalSummary)
                                .IsRequired(false)
                .HasMaxLength(1000);

            builder.HasQueryFilter(rp => !rp.IsDeleted);
        }
    }
}
