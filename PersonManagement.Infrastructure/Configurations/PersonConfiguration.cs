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
                .HasForeignKey(rp => rp.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

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

            builder.Property(p => p.BirthDay)
                .IsRequired();

            builder.HasQueryFilter(rp => !rp.IsDeleted);
        }
    }
}
