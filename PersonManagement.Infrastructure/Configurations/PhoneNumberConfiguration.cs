using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain;

namespace PersonManagement.Infrastructure.Configurations
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasKey(p => p.Id);

            // Number column
            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PhoneType)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(p => p.Person)
                .WithMany(pers => pers.PhoneNumbers)
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasQueryFilter(rp => !rp.IsDeleted);

        }
    }
}
